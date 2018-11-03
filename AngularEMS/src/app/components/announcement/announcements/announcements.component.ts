import { Component, OnInit } from '@angular/core';
import { Announcement } from '../../../services/announcement/announcement.model';
import { AnnouncementService } from '../../../services/announcement/announcement.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-announcements',
  templateUrl: './announcements.component.html',
  styleUrls: ['./announcements.component.css']
})
export class AnnouncementsComponent implements OnInit {

  grid = false;
  announcements: Array<Announcement>;
  announcementsAvailable: boolean;

  constructor(private announcementService: AnnouncementService) {
    this.getAnnouncements();
  }

  ngOnInit() {
  }

  gridMode(active: boolean) {
    this.grid = active;
  }

  gridActive() {
    if (this.grid) return '#054935';
    return '';
  }

  listActive() {
    if (!this.grid) return '#054935';
    return '';
  }

  public getAnnouncements() {
    this.announcementService.getAllAnnouncements().subscribe(
      (response: any) => {
        this.announcements = response;
        this.announcementsAvailable = this.announcements.length > 0;
        console.log(response);
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(`Error: ${errorResponse}`);
      }
    );
  }
}
