import { Injectable } from '@angular/core';
import { InfrastructureService } from '../infra/infra.service';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private infra: InfrastructureService) { }

  login(token: string, email: string, photoURL: string) {
    localStorage.setItem(this.infra.TOKEN_KEY, token);    
    localStorage.setItem(this.infra.EMAIL_KEY, email);
    localStorage.setItem(this.infra.PHOTO_URL_KEY, photoURL);
  }

  getEmail(): string {
    return localStorage.getItem(this.infra.EMAIL_KEY);
  }

  getPhotoUrl(): string {
    return localStorage.getItem(this.infra.PHOTO_URL_KEY);
  }

  isLoggedIn(): boolean {
    let token = localStorage.getItem(this.infra.TOKEN_KEY)
    return  token !== null && token !== '';
  }

  logout(): void {
    localStorage.setItem(this.infra.TOKEN_KEY, '');
    localStorage.setItem(this.infra.EMAIL_KEY, '');
    localStorage.setItem(this.infra.PHOTO_URL_KEY, '');
  }
}
