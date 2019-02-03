import { Component, AfterViewInit, ViewChild, Input } from '@angular/core';
import { Incident, Hospital } from 'src/app/services/map/map.model';
import { IncidentService } from 'src/app/services/map/incident.service';
import { HttpErrorResponse } from '@angular/common/http';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { City } from 'src/app/services/city/city.model';
import { ngxLoadingAnimationTypes } from 'ngx-loading';
import { Subscription } from 'rxjs';
import { HospitalService } from 'src/app/services/map/hospital.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements AfterViewInit {

  incidents: Incident[];

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
  currentLat = 0;
  currentLng = 0;
  kmRadius = 1.5;

  options = {
    enableHighAccuracy: true,
    timeout: 60000,
    maximumAge: 0
  };

  @ViewChild('content') content: any;
  private modalRef: NgbModalRef;

  // Slider values
  public min = 0.5;
  public max = 25;
  public smallStep = 0.5;

  // Filter ngModels
  public rangeValue = 1.5;
  public severityMinor = true;
  public severityMajor = true;
  public severityCritical = true;
  public summaryContains: string;
  public reportedBy: string;

  public incidentSubscription: Subscription;

  public severityChip: string;
  private _city: City;
  @Input()
  set city(city: City) {
    this._city = city;
    if (city) {
      this.fetchIncidents()
      .add(() => {
        if (!this.severityMinor) {
          this.incidents = this.incidents.filter(i => i.Severity !== 'Minor');
        }
        if (!this.severityMajor) {
          this.incidents = this.incidents.filter(i => i.Severity !== 'Major');
        }
        if (!this.severityCritical) {
          this.incidents = this.incidents.filter(i => i.Severity !== 'Critical');
        }
        if (this.summaryContains) {
          this.incidents = this.incidents.filter(i => i.Title.includes(this.summaryContains));
        }
        if (this.reportedBy) {
          this.incidents = this.incidents.filter(i => i.ReporterName.includes(this.reportedBy));
        }
      });
    }
  }
  get city(): City { return this._city; }

  public ngxLoadingAnimationTypes = ngxLoadingAnimationTypes;
  public loading: boolean;
  private _opened = false;

  public nearbyHospitals: Array<Hospital>;
  hospitalMarker = {
    url: '../../../assets/hospital-marker.png',
    scaledSize: {
      width: 28,
      height: 42
    }
  };

  public origin: any;
  public destination: any;

  constructor(private incidentService: IncidentService,
    private modalService: NgbModal,
    private hospitalService: HospitalService) {

    this.severityChip = 'Severity | ' +
      (this.severityMinor && 'Minor ' || '') +
      (this.severityMajor && 'Major ' || '') +
      (this.severityCritical && 'Critical' || '');

    this.loading = true;
  }

  ngAfterViewInit() {
    // Set marker to current position
    navigator.geolocation.getCurrentPosition(pos => {
      const crd = pos.coords;
      this.currentLat = crd.latitude;
      this.currentLng = crd.longitude;
      console.log(`Current position: lat ${this.currentLat} lng ${this.currentLng}`);
      // Fetch incidents and display on map
      this.origin = { lat: this.currentLat, lng: this.currentLng };
      this.fetchIncidents();
    }, (err: { code: any; message: any; }) => {
      console.warn(`Geolocation ERROR(${err.code}): ${err.message}`);
      this.loading = false;
    }, this.options);
  }

  getBGColor(incident: Incident) {
    let color = 'rgba(255, 214, 0, 0.8)';
    switch (incident.Severity) {
      case 'Minor':
        color = 'rgba(255, 214, 0, 0.8)';
        break;
      case 'Major':
        color = 'rgba(255, 96, 0, 0.7)';
        break;
      case 'Critical':
        color = 'rgba(255, 22, 22, 0.7)';
        break;
    }
    return color;
  }

  openFilterModal(content: any) {
    this.modalRef = this.modalService.open(content);
  }

  applyFilters() {
    this.severityChip = 'Severity | ' +
      (this.severityMinor && 'Minor ' || '') +
      (this.severityMajor && 'Major ' || '') +
      (this.severityCritical && 'Critical' || '');
    this.kmRadius = this.rangeValue;
    this.modalRef.close();

    this.loading = true;

    console.log('Called here too (2)');
    this.fetchIncidents()
    .add(() => {
      if (!this.severityMinor) {
        this.incidents = this.incidents.filter(i => i.Severity !== 'Minor');
      }
      if (!this.severityMajor) {
        this.incidents = this.incidents.filter(i => i.Severity !== 'Major');
      }
      if (!this.severityCritical) {
        this.incidents = this.incidents.filter(i => i.Severity !== 'Critical');
      }
      if (this.summaryContains) {
        this.incidents = this.incidents.filter(i => i.Title.includes(this.summaryContains));
      }
      if (this.reportedBy) {
        this.incidents = this.incidents.filter(i => i.ReporterName.includes(this.reportedBy));
      }
    });
  }

  async resetToDefault() {
    console.log('Called here too (3)');
    await new Promise((resolve, reject) => {
      this.loading = true;
      this.city = null;
      this.incidents = [];
      this.kmRadius = 1.5;
      this.severityMinor = this.severityMajor = this.severityCritical = true;
      this.severityChip = 'Severity | ' +
        (this.severityMinor && 'Minor ' || '') +
        (this.severityMajor && 'Major ' || '') +
        (this.severityCritical && 'Critical' || '');
      this.summaryContains = null;
      this.reportedBy = null;
      resolve();
    }).then(() => this.fetchIncidents());
  }

  fetchIncidents() {
    let lat = this.currentLat, lng = this.currentLng;
    if (this.city) {
      lat = parseFloat(this.city.latitude);
      lng = parseFloat(this.city.longitude);
      this.kmRadius = 10;
    }
    console.log(this.city, this.kmRadius);
    if (this.incidentSubscription && !this.incidentSubscription.closed) {
      this.incidentSubscription.unsubscribe();
    }
    return this.incidentSubscription = this.incidentService.getAllIncidentsWithinARadius(lat, lng, this.kmRadius)
      .subscribe(
      (response: any) => {
        this.incidents = response;
        console.log('Incidents: ', this.incidents);
        this.loading = false;
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(`Error (Incidents): ${errorResponse}`);
        this.loading = false;
      }
    );
  }

  displayNearHospitals() {
    this.loading = true;
    const lat = this.currentLat, lng = this.currentLng;
    this.hospitalService.getNearbyHospitals(String(lat), String(lng), String(this.kmRadius * 1000))
    .subscribe(
      (response: any) => {
        this.nearbyHospitals = response;
        this.loading = false;
        console.log('Nearby hospitals: ', this.nearbyHospitals);
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(`Error (Neaby Hospitals): ${errorResponse}`);
        this.loading = false;
      }
    );
  }

  setDestinationPoint(lat: any, lng: any) {
    this.destination = { lat: lat, lng: lng };
  }

  private _toggleSidebar() {
    this._opened = !this._opened;
  }
}
