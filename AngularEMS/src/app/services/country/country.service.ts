import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { Country } from './country.model';
import { RootUrlService } from '../root-url/root-url.service';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  readonly url = this.rootUrl.URL + '/api/countries';

  constructor(private http: HttpClient,
              private rootUrl: RootUrlService) { }

  getAllCountries(): Observable<Array<Country>> {
    return this.http.get<Array<Country>>(this.url);
  }
}
