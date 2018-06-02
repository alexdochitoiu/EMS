import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { InfrastructureService } from '../infra/infra.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public email: string;

  constructor(private router: Router,
              private infra: InfrastructureService) { }

  login(token: string, email: string) {
    localStorage.setItem(this.infra.TOKEN_KEY, token);
    this.email = email;
  }

  getEmail(): string {
    return this.email;
  }

  isLogged(): boolean {
    let token = localStorage.getItem(this.infra.TOKEN_KEY)
    return  token !== null && token !== '';
  }

  logout(): void {
    localStorage.setItem(this.infra.TOKEN_KEY, '');
    this.email = null;
  }
}
