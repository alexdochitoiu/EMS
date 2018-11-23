import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  loggedIn: boolean;
  email: string;
  photo: string;

  constructor(private auth: AuthService,
              private router: Router) {
    this.loggedIn = this.auth.isLoggedIn();
    this.email = this.auth.getEmail();
    this.photo = this.auth.getPhotoUrl();
   }

  ngOnInit() {
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);
  }

  goToRoute(path) {
    this.router.navigate(['/']).then(() => {
      this.router.navigate([path]);
    });
  }
}
