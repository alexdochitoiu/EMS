import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { UserModel } from '../../services/user/user.model';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user/user.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  user: UserModel;
  profilePhoto: string;
  isAvailable: boolean;

  constructor(private route: ActivatedRoute,
              private authService: AuthService,
              private userService: UserService) {
    this.profilePhoto = this.authService.getPhotoUrl();
    this.route.params.subscribe(params => {
      this.getUser(params['username']);
    });
   }

  ngOnInit() {
  }

  getUser(username: string) {
    this.userService.userByUsername(username).subscribe(
      (response: UserModel) => {
        this.user = response;
        this.isAvailable = true;
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(errorResponse);
      }
    );
  }
}
