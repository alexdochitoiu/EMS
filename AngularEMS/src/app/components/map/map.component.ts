import { Component, OnInit, ViewChild } from '@angular/core';
import { Incident } from 'src/app/services/map/map.model';
import { IncidentService } from 'src/app/services/map/incident.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

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
    timeout: 5000,
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

  public severityChip: string;

  constructor(private incidentService: IncidentService,
    private router: Router,
    private modalService: NgbModal) {
    this.severityChip = 'Severity | ' +
      (this.severityMinor && 'Minor ' || '') +
      (this.severityMajor && 'Major ' || '') +
      (this.severityCritical && 'Critical' || '');
  }

  ngOnInit() {
    // Set marker to current position
    navigator.geolocation.getCurrentPosition(pos => {
      const crd = pos.coords;
      this.currentLat = crd.latitude;
      this.currentLng = crd.longitude;
      console.log(`Current position: lat ${this.currentLat} lng ${this.currentLng}`);
      // Fetch incidents and display on map
      this.fetchIncidents();
    }, this.error, this.options);
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

  error(err: { code: any; message: any; }) {
    console.warn(`ERROR(${err.code}): ${err.message}`);
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

  resetToDefault() {
    console.log('reset filters');
    this.kmRadius = 1.5;
    this.severityMinor = true;
    this.severityMajor = true;
    this.severityCritical = true;
    this.severityChip = 'Severity | ' +
      (this.severityMinor && 'Minor ' || '') +
      (this.severityMajor && 'Major ' || '') +
      (this.severityCritical && 'Critical' || '');
    this.summaryContains = null;
    this.reportedBy = null;
    this.fetchIncidents();
  }

  fetchIncidents() {
    return this.incidentService.getAllIncidentsWithinARadius(
      this.currentLat,
      this.currentLng,
      this.kmRadius)
      .subscribe(
      (response: any) => {
        this.incidents = response;
        console.log(response);
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(`Error (Incidents): ${errorResponse}`);
      }
    );
  }
}
