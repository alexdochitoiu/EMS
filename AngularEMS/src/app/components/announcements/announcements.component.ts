import { Component, OnInit } from '@angular/core';
import { Announcement } from '../../services/announcement/announcement.model';

@Component({
  selector: 'app-announcements',
  templateUrl: './announcements.component.html',
  styleUrls: ['./announcements.component.css']
})
export class AnnouncementsComponent implements OnInit {

  collection: Announcement[] = [];
  grid = false;
  constructor() {
    for (let i = 1; i <= 1000; i++) {
      const a: Announcement = {
        Title: 'Title ' + i,
        Description: 'Description ' + i,
        PostedBy: 'User'+i,
        PostedAt: 'DateTime'+i,
        Severity: i % 3
      };
      this.collection.push(a);
    }
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
}
