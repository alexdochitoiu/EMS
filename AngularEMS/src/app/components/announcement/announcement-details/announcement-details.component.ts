import { Component, OnInit } from '@angular/core';
import { Announcement } from '../../../services/announcement/announcement.model';
import { ActivatedRoute } from '@angular/router';
import { AnnouncementService } from '../../../services/announcement/announcement.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-announcement-details',
  templateUrl: './announcement-details.component.html',
  styleUrls: ['./announcement-details.component.css']
})
export class AnnouncementDetailsComponent implements OnInit {

  isAvailable: boolean;
  announcement: Announcement;

  constructor(private route: ActivatedRoute,
              private announcementService: AnnouncementService) {
    this.route.params.subscribe(params => 
    {
      this.getAnnouncement(params['id']);
    }); 
  }

  ngOnInit() { 
  }

  public getAnnouncement(id: string) {
    this.announcementService.getAnnouncement(id).subscribe(
      (response: any) => {
        this.announcement = response;
        this.isAvailable = true;
        console.log(response);
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(errorResponse);
      }
    );
  }
}
