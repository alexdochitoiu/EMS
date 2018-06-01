import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { InfrastructureService } from '../infra/infra.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private router: Router,
              private infra: InfrastructureService) { }

  login(token: string) {
    localStorage.setItem(this.infra.TOKEN_KEY, token);
  }

  isLogged(): boolean {
    let token = localStorage.getItem(this.infra.TOKEN_KEY)
    return  token !== null && token !== '';
  }

  logout(): void {
    localStorage.setItem(this.infra.TOKEN_KEY, '');
  }
}
