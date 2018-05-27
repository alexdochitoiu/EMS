import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit {

  public listItems: Array<string> = ['X-Small', 'Small', 'Medium', 'Large', 'X-Large', '2X-Large'];

  constructor() { }

  ngOnInit() {
  }

}
