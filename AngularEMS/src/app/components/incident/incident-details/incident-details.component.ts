import { Component, OnInit, ViewChild } from '@angular/core';
import { IncidentService } from 'src/app/services/map/incident.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ngxLoadingAnimationTypes } from 'ngx-loading';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-incident-details',
  templateUrl: './incident-details.component.html',
  styleUrls: ['./incident-details.component.css']
})
export class IncidentDetailsComponent implements OnInit {

  incident: any;
  images: File[];
  isAvailable: boolean;
  severity: string;
  imageSrc: string;

  minorSeverityIcon = {
    url: '../../../assets/yellow-marker.png',
    scaledSize: {
      width: 25,
      height: 42
    },
  };

  majorSeverityIcon = {
    url: '../../../assets/orange-marker.png',
    scaledSize: {
      width: 25,
      height: 42
    },
  };

  public ngxLoadingAnimationTypes = ngxLoadingAnimationTypes;
  public loading: boolean;

  @ViewChild('content') content: any;
  private modalRef: NgbModalRef;

  constructor(private incidentService: IncidentService,
    private route: ActivatedRoute,
    private router: Router,
    private modalService: NgbModal) { }

  ngOnInit() {
    this.loading = true;
    this.route.params.subscribe(params => {
      this.incidentService.getIncident(params['id']).subscribe(
        (response: any) => {
          this.incident = response;
          console.log(response);
          this.isAvailable = true;
          this.severity = this.incident['severity'] === 2 ? 'Minor' :
            this.incident['severity'] === 1 ? 'Major' : 'Critical';
          this.loading = false;
        }
      );
      this.incidentService.getIncidentPhotos(params['id']).subscribe(
        (response: any) => {
          this.images = response;
          this.loading = false;
        }
      );
    });
  }

  openImagePreview(content: any, image: any) {
    this.imageSrc = image;
    console.log(this.imageSrc);
    this.modalRef = this.modalService.open(content);
  }

  getImageSrc(image: any) {
    return 'data:' + image.contentType + ';base64,' + image.fileContents;
  }

  public getColor() {
    switch (this.incident['severity']) {
      case 0: return 'red';
      case 1: return 'orange';
      case 2: return 'yellow';
    }
  }

  getMarker() {
    switch (this.incident['severity']) {
      case 0: return null;
      case 1: return this.majorSeverityIcon;
      case 2: return this.minorSeverityIcon;
    }
  }

  navigateToUserProfile(username: string) {
    this.router.navigate(['users', username]);
  }
}
