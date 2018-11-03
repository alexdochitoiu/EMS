import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { City } from './city.model';
import { InfrastructureService } from '../infra/infra.service';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  constructor(private http: HttpClient,
              private infra: InfrastructureService) { }

  getAllCities(country: string): Observable<Array<City>> {
    return this.http.get<Array<City>>(this.infra.URL + '/api/countries/' + country + '/cities');
  }
}
