import { Component, OnInit } from '@angular/core';
import { MouseEvent as AGMMouseEvent } from '@agm/core';
import { CreateIncidentModel } from 'src/app/services/map/map.model';
import { IncidentService } from 'src/app/services/map/incident.service';
import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from 'src/app/services/user/user.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { AlertService } from 'src/app/services/alert/alert.service';
import { InfrastructureService } from 'src/app/services/infra/infra.service';
import { ngxLoadingAnimationTypes } from 'ngx-loading';

@Component({
  selector: 'app-report-incident',
  templateUrl: './report-incident.component.html',
  styleUrls: ['./report-incident.component.css']
})
export class ReportIncidentComponent implements OnInit {

  incident: CreateIncidentModel;
  imagesToUpload: File[];
  reporterId: string;
  errorMessage: string;
  successMessage: string;
  checked = true;
  currentLat = 0;
  currentLng = 0;
  incidentLat = -1;
  incidentLng = -1;

  public ngxLoadingAnimationTypes = ngxLoadingAnimationTypes;
  public loading: boolean;

  currentPositionIcon = {
    url: '../../../assets/current-position-marker.png',
    scaledSize: {
      width: 20,
      height: 20
    },
    origin: {
      x: 0,
      y: 0
    },
    anchor: {
      x: 10,
      y: 10
    }
  };
  options = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 0
  };
  public reportIncidentRadius: number;

  constructor(private incidentService: IncidentService,
    private userService: UserService,
    private authService: AuthService,
    private alertService: AlertService,
    private infrastructure: InfrastructureService) {
    this.incident = new CreateIncidentModel();
    this.imagesToUpload = [];
    this.incident.Severity = 2;
    this.reportIncidentRadius = 500;
  }

  ngOnInit() {
    navigator.geolocation.getCurrentPosition(pos => {
      const crd = pos.coords;
      this.currentLat = crd.latitude;
      this.currentLng = crd.longitude;

    }, this.error, this.options);
  }

  mapClicked($event: AGMMouseEvent) {
    if (this.checked === true) { return; }
    this.incidentLat = $event.coords.lat;
    this.incidentLng = $event.coords.lng;
  }

  handleSeverityChange(value: number) {
    switch (value) {
      case 0: this.reportIncidentRadius = 3000; break;
      case 1: this.reportIncidentRadius = 1500; break;
      default: this.reportIncidentRadius = 500;
    }
  }

  radiusChange($event: any) {
    this.reportIncidentRadius = $event;
  }

  public receiveFile($event: File) {
    this.imagesToUpload.push($event);
  }

  async onSubmit() {
    this.loading = true;
    this.incident.Latitude = this.checked ? this.currentLat : this.incidentLat;
    this.incident.Longitude = this.checked ? this.currentLng : this.incidentLng;

    if (this.incident.Latitude === -1 && this.incident.Longitude === -1) {
      this.errorMessage = 'Please click on map to select the incident position';
    }

    await this.userService.userByEmail(this.authService.getEmail())
      .subscribe(
      async (response: any) => {
        this.incident.ReporterId = response.id;
        await this.incidentService.createIncident(this.incident)
          .subscribe(
          async (res: any) => {

            // Upload images
            const images = this.imagesToUpload.map(x => {
              return {
                base64String: x['dataURL']
              };
            });
            const model = {
              incidentId: res.id,
              images: images
            };
            await this.incidentService.uploadPhotos(model)
            .subscribe(
              (uploadResult: any) => {
                console.log(uploadResult);
                this.successMessage = 'The incident was successfully reported!';
                this.loading = false;
              },
              (uploadErrorResponse: HttpErrorResponse) => {
                console.log(uploadErrorResponse);
                this.loading = false;
              }
            );

            const severity =
              this.incident.Severity === 0 ? 'Critical' :
              this.incident.Severity === 1 ? 'Major' : 'Minor';
            const incidentLink = `${this.infrastructure.HOST_ADDRESS}/incidents/${res.id}`;
            let bodyMessage = 'An incident have been reported near you:';
            bodyMessage += `\n- Summary: '${this.incident.Summary}'`;
            bodyMessage += `\n- Severity: '${severity}'\n`;
            bodyMessage += `\nClick the follow link to see details: ${incidentLink}`;
            const alertModel = {
              radius: String(this.reportIncidentRadius),
              latitude: String(this.incident.Latitude),
              longitude: String(this.incident.Longitude),
              message: bodyMessage
            };
            await this.alertService.alertNearbyUsers(alertModel)
            .subscribe(
              (alertResponse: any) => {
                console.log('Users successfully alerted via SMS:', alertResponse, alertModel);
                this.loading = false;
              },
              (alertErrorResponse: HttpErrorResponse) => {
                this.errorMessage = alertErrorResponse.error;
                console.log('Alert call function error: ', alertErrorResponse);
                this.loading = false;
              }
            );
          },
          (errorResponse: HttpErrorResponse) => {
            this.errorMessage = errorResponse.error;
            console.log(errorResponse);
            this.loading = false;
          }
        );
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(errorResponse);
        this.loading = false;
      }
    );
  }

  error(err: { code: any; message: any; }) {
    console.warn(`ERROR(${err.code}): ${err.message}`);
  }
}
