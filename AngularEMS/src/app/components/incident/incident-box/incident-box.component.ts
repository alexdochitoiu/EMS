import { Component, AfterViewInit, Input, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-incident-box',
  templateUrl: './incident-box.component.html',
  styleUrls: ['./incident-box.component.css']
})
export class IncidentBoxComponent implements AfterViewInit {

  @Input() incident: any;
  @ViewChild('icon') icon: ElementRef;
  constructor(private router: Router) { }

  ngAfterViewInit() {
    switch (this.incident.Severity) {
      case 'Critical': this.icon.nativeElement.style.color = 'red'; break;
      case 'Major': this.icon.nativeElement.style.color = 'orange'; break;
      default: this.icon.nativeElement.style.color = 'yellow'; break;
    }
  }

  navigateToUserProfile(username: string) {
    this.router.navigate(['users', username]);
  }

  navigateToIncidentDetails(id: string) {
    this.router.navigate(['incidents', id]);
  }

}
