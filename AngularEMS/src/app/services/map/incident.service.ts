import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { InfrastructureService } from '../infra/infra.service';
import { Incident } from './map.model';
import { Observable } from 'rxjs';
import { map, share } from 'rxjs/operators';

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
    return this.http.get<Array<Incident>>(this.url)
      .pipe(map(array => array
        .map((i: any) => {
          const severity = i.severity === 0 ? 'Critical' :
            i.severity === 1 ? 'Major' : 'Minor';
          return new Incident(i.latitude, i.longitude, severity, i.title, i.description, i.reporter.username, i.id);
        })), share());
  }

  createIncident(incident: any) {
    this.url = this.infra.URL + '/api/incidents';
    return this.http.post(this.url, incident).pipe(map((response: Response) => <any>response));
  }

  uploadPhotos(imageBase64: any) {
    this.url = this.infra.URL + '/api/upload';
    return this.http.post(this.url, imageBase64, { responseType: 'text' });
  }

  getIncidentPhotos(incidentId: string): Observable<Array<File>> {
    this.url = this.infra.URL + '/api/upload/' + incidentId;
    return this.http.get(this.url).pipe(map((response: Response) => <any>response));
  }

  getIncident(incidentId: string): Observable<Incident> {
    this.url = this.infra.URL + '/api/incidents/' + incidentId;
    return this.http.get(this.url).pipe(map((response: Response) => <any>response));
  }
}
