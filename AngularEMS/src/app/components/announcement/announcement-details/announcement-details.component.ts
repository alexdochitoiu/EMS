import { Component, OnInit, ViewChild } from '@angular/core';
import { Announcement } from '../../../services/announcement/announcement.model';
import { ActivatedRoute, Router } from '@angular/router';
import { AnnouncementService } from '../../../services/announcement/announcement.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ngxLoadingAnimationTypes } from 'ngx-loading';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-announcement-details',
  templateUrl: './announcement-details.component.html',
  styleUrls: ['./announcement-details.component.css']
})
export class AnnouncementDetailsComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private announcementService: AnnouncementService,
              private router: Router,
              private modalService: NgbModal) {
    this.route.params.subscribe(params => {
      this.getAnnouncement(params['id']);
    });
  }

  isAvailable: boolean;
  announcement: Announcement;
  severity: string;

  public ngxLoadingAnimationTypes = ngxLoadingAnimationTypes;
  public loading: boolean;

  @ViewChild('content') content: any;
  private modalRef: NgbModalRef;

  ngOnInit() {
  }

  openImagePreview(content: any) {
    this.modalRef = this.modalService.open(content);
  }

  public getAnnouncement(id: string) {
    this.loading = true;
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
        this.loading = false;
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
      default: return 'yellow';
    }
  }

  navigateToUserProfile(username: string) {
    this.router.navigate(['users', username]);
  }
}
