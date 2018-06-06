import { Component, ElementRef, AfterViewInit, Input, ViewChild } from '@angular/core';
import { Announcement } from '../../../services/announcement/announcement.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-announcement-box',
  templateUrl: './announcement-box.component.html',
  styleUrls: ['./announcement-box.component.css']
})
export class AnnouncementBoxComponent implements AfterViewInit {

  @Input() announcement: Announcement;
  @ViewChild('icon') icon: ElementRef;
  constructor(private router: Router) { }

  ngAfterViewInit() {
    switch(this.announcement.Severity) {
      case 0: this.icon.nativeElement.style.color = 'red'; break;
      case 1: this.icon.nativeElement.style.color = 'orange'; break;
      default: this.icon.nativeElement.style.color = '#0d9e73'; break;
    
    }
  }

  navigateToUserProfile(username: string) {
    this.router.navigate(['users', username])
  }

  navigateToAnnouncementDetails(id: string) {
    this.router.navigate(['announcements', id])
  }
}
