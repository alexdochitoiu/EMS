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

  public defaultCountryItem: Country = {
    name: 'Select country',
    abbreviation: 'NA'
  };

  public defaultCityItem: City = {
    name: 'Select city',
    abbreviation: 'NA'
  };

  public dataCountries: Array<Country> = new Array<Country>();
  public dataCities: Array<City> = new Array<City>();

  public selectedCountry: Country;
  public selectedCity: City;

  constructor(private countryService: CountryService,
              private cityService: CityService) {  }

  ngOnInit() {
    this.getCountries();
  }

  handleCountryChange(value) {
    this.selectedCountry = value;
    this.selectedCity = undefined;

    if (value !== this.defaultCountryItem) {
        this.getCities(value.name);
    }
  }

  handleCityChange(value) {
    this.selectedCity = value;
  }

  public getCountries() {
    this.countryService.getAllCountries().subscribe(
      (response) => {
          this.dataCountries = response;
      },
      err => {
          console.log(err);
      }
    );
  }

  public getCities(countryName: string) {
    this.cityService.getAllCities(countryName).subscribe(
      (response) => {
          this.dataCities = response;
      },
      err => {
          console.log(err);
      }
    );
  }
}
