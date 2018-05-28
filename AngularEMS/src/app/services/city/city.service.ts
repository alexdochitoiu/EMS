import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { City } from './city.model';
import { RootUrlService } from '../root-url/root-url.service';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  constructor(private http: HttpClient,
              private rootUrl: RootUrlService) { }

  getAllCities(country: string): Observable<Array<City>> {
    return this.http.get<Array<City>>(this.rootUrl.URL + '/api/countries/' + country + '/cities');
  }
}
