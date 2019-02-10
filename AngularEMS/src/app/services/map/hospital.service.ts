import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { InfrastructureService } from '../infra/infra.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Hospital } from './map.model';

@Injectable({
  providedIn: 'root'
})
export class HospitalService {

  private url: string;

  constructor(private http: HttpClient,
    private infraService: InfrastructureService) { }

  getNearbyHospitals(lat: string, lng: string, radius: string): Observable<Array<Hospital>> {
    this.url = 'https://maps.googleapis.com/maps/api/place/nearbysearch/json?';
    this.url += `location=${lat},${lng}&radius=${radius}&`;
    this.url += `type=hospital&key=${this.infraService.GOOGLE_MAPS_API_KEY}`;
    return this.http.get<Array<Hospital>>(this.url).pipe(map(res =>
      res['results'].map(h => new Hospital(
        res['results'].indexOf(h),
        h.geometry.location.lat,
        h.geometry.location.lng,
        h.name,
        h.vicinity
      ))));
  }

  getDistance (origin: any, destination: any) {
    this.url = 'https://maps.googleapis.com/maps/api/distancematrix/json?';
    this.url += `origins=${origin.lat},${origin.lng}&`;
    this.url += `destinations=${destination.lat},${destination.lng}&`;
    this.url += `key=${this.infraService.GOOGLE_MAPS_API_KEY}`;
    return this.http.get(this.url).pipe(map(res => res['rows'][0]['elements'][0]));
  }

}
