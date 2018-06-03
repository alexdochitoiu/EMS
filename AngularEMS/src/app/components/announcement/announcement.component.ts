import { Component, ElementRef, AfterViewInit, Input, ViewChild } from '@angular/core';
import { Announcement } from '../../services/announcement/announcement.model';

@Component({
  selector: 'app-announcement',
  templateUrl: './announcement.component.html',
  styleUrls: ['./announcement.component.css']
})
export class AnnouncementComponent implements AfterViewInit {

  @Input() announcement: Announcement;
  @ViewChild('icon') icon: ElementRef;
  constructor() { }

  ngAfterViewInit() {
    switch(this.announcement.Severity) {
      case 0: this.icon.nativeElement.style.color = 'red'; break;
      case 1: this.icon.nativeElement.style.color = 'orange'; break;
      default: this.icon.nativeElement.style.color = '#0d9e73'; break;
    
    }
  }
}
