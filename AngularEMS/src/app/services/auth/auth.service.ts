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
    return localStorage.getItem(this.infra.TOKEN_KEY) !== '';
  }

  logout(): void {
    localStorage.setItem(this.infra.TOKEN_KEY, '');
    this.router.navigate(['/login']);
  }
}
