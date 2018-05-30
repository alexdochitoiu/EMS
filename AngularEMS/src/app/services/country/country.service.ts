import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { Country } from './country.model';
import { InfrastructureService } from '../infra/infra.service';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  readonly url = this.infra.URL + '/api/countries';

  constructor(private http: HttpClient,
              private infra: InfrastructureService) { }

  getAllCountries(): Observable<Array<Country>> {
    return this.http.get<Array<Country>>(this.url);
  }
}
