import { Injectable } from '@angular/core';
import { InfrastructureService } from '../infra/infra.service';
import { Announcement } from './announcement.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { Title } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {

  url: string;

  constructor(private http: HttpClient,
              private infra: InfrastructureService) { }

  getAllAnnouncements(): Observable<Array<Announcement>> {
    this.url = this.infra.URL + '/api/announcements';
    return this.http.get<Array<Announcement>>(this.url)
      .map(array => array
        .map((a: any) => 
          new Announcement(a.id, a.title, a.description, a.user, a.created, a.severity)));              
  }

  getAnnouncement(id: string): Observable<Announcement> {
    this.url = this.infra.URL + '/api/announcements/' + id;
    return this.http.get<Announcement>(this.url)
      .map((a: any) =>
        new Announcement(a.id, a.title, a.description, a.user, a.created, a.severity));
  }
  
  getUsersAnnouncements(userId: string): Observable<Array<Announcement>> {
    this.url = this.infra.URL + '/api/users/' + userId + '/announcements';
    return this.http.get<Array<Announcement>>(this.url);
  }
}
