import { Component, OnInit } from '@angular/core';

import { CountryService } from '../../services/country/country.service';
import { Country } from '../../services/country/country.model';

import { CityService } from '../../services/city/city.service';
import { City } from '../../services/city/city.model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  countries: Array<Country> = new Array<Country>();
  cities: Array<City> = new Array<City>();
  selectedCountry: string = null;
  selectedCity: string = null;

  constructor(private countryService: CountryService,
              private cityService: CityService) {  }

  ngOnInit() {
    this.getCountries();
  }

  getCountries() {
    this.countryService.getAllCountries().subscribe(
      (response) => {
          this.countries = response;
      },
      err => {
          console.log(err);
      }
    );
  }

  getCountriesNames() {
    return this.countries.map(
      (c) => {
        return c.name;
      });
  }

  getCities() {
    this.selectedCity = 'Select city';
    if (this.selectedCountry === 'Select country') {
      this.cities = null;
      return;
    }
    this.cityService.getAllCities(this.selectedCountry).subscribe(
      (response) => {
          this.cities = response;
      },
      err => {
          console.log(err);
      }
    );
  }

  getCitiesNames() {
    return this.cities ? this.cities.map(
      (c) => {
        return c.name;
      }) : null;
  }
}
