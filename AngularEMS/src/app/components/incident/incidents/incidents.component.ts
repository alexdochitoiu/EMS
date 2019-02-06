import { Component, OnInit } from '@angular/core';
import { ngxLoadingAnimationTypes } from 'ngx-loading';
import { IncidentService } from 'src/app/services/map/incident.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-incidents',
  templateUrl: './incidents.component.html',
  styleUrls: ['./incidents.component.css']
})
export class IncidentsComponent implements OnInit {

  public ngxLoadingAnimationTypes = ngxLoadingAnimationTypes;
  public loading: boolean;
  incidents: any;
  incidentsAvailable: boolean;

  constructor(private incidentService: IncidentService) { }

  ngOnInit() {
    this.loading = false;
    this.getIncidents();
  }

  public getIncidents() {
    this.loading = true;
    this.incidentService.getAllIncidents().subscribe(
      (response: any) => {
        this.incidents = response;
        this.incidentsAvailable = this.incidents.length > 0;
        console.log(response);
        this.loading = false;
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(`Error: ${errorResponse}`);
        this.loading = false;
      }
    );
  }
}
