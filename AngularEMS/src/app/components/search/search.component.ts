import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';

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
    abbreviation: 'NA',
    latitude: '0',
    longitude: '0'
  };

  public dataCountries: Array<Country> = new Array<Country>();
  public dataCities: Array<City> = new Array<City>();

  public selectedCountry: Country;
  public selectedCity: City;

  @Output() selected = new EventEmitter<City>();

  constructor(private countryService: CountryService,
              private cityService: CityService) {
  }

  ngOnInit() {
    this.getCountries();
  }

  handleCountryChange(value: Country) {
    this.selectedCountry = value;
    this.selectedCity = undefined;
    this.dataCities = null;

    if (value !== this.defaultCountryItem) {
        this.getCities(value.name);
    }
  }

  handleCityChange(value: City) {
    this.selectedCity = value;
  }

  public getCountries() {
    this.countryService.getAllCountries().subscribe(
      (response: any) => {
          this.dataCountries = response;
      },
      (errorResponse: HttpErrorResponse) => {
          console.log(errorResponse);
      }
    );
  }

  public getCities(countryName: string) {
    this.cityService.getAllCities(countryName).subscribe(
      (response: any) => {
        this.dataCities = response;
        // console.log(this.dataCities);
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(errorResponse);
      }
    );
  }

  public searchIncidents(city: City) {
    if (city) {
      this.selected.emit(city);
      this.selectedCountry = this.defaultCountryItem;
      this.selectedCity = this.defaultCityItem;
    }
  }
}
