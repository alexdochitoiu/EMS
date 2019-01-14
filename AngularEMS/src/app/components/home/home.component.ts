import { Component, OnInit } from '@angular/core';
import { City } from 'src/app/services/city/city.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public selectedCity: City;
  constructor() { }

  ngOnInit() {
  }
}
