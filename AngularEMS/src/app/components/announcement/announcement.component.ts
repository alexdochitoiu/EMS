import { Component, OnInit } from '@angular/core';
import { Announcement } from '../../services/announcement/announcement.model';

@Component({
  selector: 'app-announcement',
  templateUrl: './announcement.component.html',
  styleUrls: ['./announcement.component.css']
})
export class AnnouncementComponent implements OnInit {

  public announcement: Announcement;

  constructor() { }

  ngOnInit() {
    this.announcement =  {
      Title: 'Titlu - test',
      Description: 'Descriere - test',
      PostedBy: 'Grigore Ureche',
      PostedAt: 'Friday, 13.05.2018 16:15',
      Severity: 2
    };
  }

}
