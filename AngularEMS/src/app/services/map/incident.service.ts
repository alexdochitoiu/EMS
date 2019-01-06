import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { InfrastructureService } from '../infra/infra.service';
import { Incident, CreateIncidentModel } from './map.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class IncidentService {

  url: string;

  constructor(private http: HttpClient,
    private infra: InfrastructureService) { }

  getAllIncidents(): Observable<Array<Incident>> {
  this.url = this.infra.URL + '/api/incidents';
  return this.http.get<Array<Incident>>(this.url)
    .pipe(map(array => array
      .map((i: any) => {
        const severity = i.severity === 0 ? 'Critical' :
          i.severity === 1 ? 'Major' : 'Minor';
        return new Incident(i.latitude, i.longitude, severity, i.title, i.description, i.reporter.username, i.id);
      })));
  }

  getAllIncidentsWithinARadius(lat: number, lng: number, km: number): Observable<Array<Incident>> {
    this.url = this.infra.URL + '/api/incidents/radius?';
    this.url += 'CenterLatitude=' + lat + '&';
    this.url += 'CenterLongitude=' + lng + '&';
    this.url += 'Kilometers=' + km;
    console.log(lat, lng);
    console.log(this.url);
    return this.http.get<Array<Incident>>(this.url)
      .pipe(map(array => array
        .map((i: any) => {
          const severity = i.severity === 0 ? 'Critical' :
            i.severity === 1 ? 'Major' : 'Minor';
          return new Incident(i.latitude, i.longitude, severity, i.title, i.description, i.reporter.username, i.id);
        })));
  }

  createIncident(incident: any) {
    this.url = this.infra.URL + '/api/incidents';
    return this.http.post(this.url, incident).pipe(map((response: Response) => <any>response));
  }
}
