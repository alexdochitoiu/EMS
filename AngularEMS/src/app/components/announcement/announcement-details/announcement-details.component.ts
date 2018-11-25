import { Component, OnInit } from '@angular/core';
import { Announcement } from '../../../services/announcement/announcement.model';
import { ActivatedRoute, Router } from '@angular/router';
import { AnnouncementService } from '../../../services/announcement/announcement.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Image } from '@ks89/angular-modal-gallery';

@Component({
  selector: 'app-announcement-details',
  templateUrl: './announcement-details.component.html',
  styleUrls: ['./announcement-details.component.css']
})
export class AnnouncementDetailsComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private announcementService: AnnouncementService,
              private router: Router) {
    this.route.params.subscribe(params => {
      this.getAnnouncement(params['id']);
    });
  }

  isAvailable: boolean;
  announcement: Announcement;
  severity: string;

  images: Image[] = [
    new Image(
      0,
      { // modal
        img: 'https://raw.githubusercontent.com/Ks89/angular-modal-gallery/v4/examples/systemjs/assets/images/gallery/img1.jpg',
        extUrl: 'http://www.google.com'
      }
    ),
    new Image(
      1,
      { // modal
        img: 'https://raw.githubusercontent.com/Ks89/angular-modal-gallery/v4/examples/systemjs/assets/images/gallery/img2.png',
        description: 'Description 2'
      }
    ),
    new Image(
      2,
      { // modal
        img: 'https://raw.githubusercontent.com/Ks89/angular-modal-gallery/v4/examples/systemjs/assets/images/gallery/img3.jpg',
        description: 'Description 3',
        extUrl: 'http://www.google.com'
      },
      { // plain
        img: 'https://raw.githubusercontent.com/Ks89/angular-modal-gallery/v4/examples/systemjs/assets/images/gallery/thumbs/img3.png',
        title: 'custom title 2',
        alt: 'custom alt 2',
        ariaLabel: 'arial label 2'
      }
    ),
    new Image(
      3,
      { // modal
        img: 'https://raw.githubusercontent.com/Ks89/angular-modal-gallery/v4/examples/systemjs/assets/images/gallery/img4.jpg',
        description: 'Description 4',
        extUrl: 'http://www.google.com'
      }
    ),
    new Image(
      4,
      { // modal
        img: 'https://raw.githubusercontent.com/Ks89/angular-modal-gallery/v4/examples/systemjs/assets/images/gallery/img5.jpg'
      },
      { // plain
        img: 'https://raw.githubusercontent.com/Ks89/angular-modal-gallery/v4/examples/systemjs/assets/images/gallery/thumbs/img5.jpg'
      }
    )
  ];

  ngOnInit() {
  }

  public getAnnouncement(id: string) {
    this.announcementService.getAnnouncement(id).subscribe(
      (response: any) => {
        this.announcement = response;
        switch (this.announcement.Severity) {
          case 0: this.severity = 'Critical'; break;
          case 1: this.severity = 'Major'; break;
          case 2: this.severity = 'Minor'; break;
          default: this.severity = 'Unknown'; break;
        }
        this.isAvailable = true;
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(errorResponse);
      }
    );
  }

  public getColor() {
    switch (this.announcement.Severity) {
      case 0: return 'red';
      case 1: return 'orange';
      default: return '#0d9e73';
    }
  }

  navigateToUserProfile(username: string) {
    this.router.navigate(['users', username]);
  }
}
