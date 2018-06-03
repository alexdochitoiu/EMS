import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from '@angular/forms/src/directives';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { UserLoginModel, UserRegisterModel } from '../../../services/user/user.model';
import { UserService } from '../../../services/user/user.service';
import { AuthService } from '../../../services/auth/auth.service';
import { InfrastructureService } from '../../../services/infra/infra.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  errors: Array<string>;
  user: UserLoginModel;
  event: MouseEvent;

  constructor(private userService: UserService,
              private authService: AuthService,
              private router: Router,
              private infra: InfrastructureService) {
    this.user = new UserLoginModel();
    this.event = new MouseEvent('click', {bubbles: true});
  }

  @ViewChild('login') loginModal;
  @ViewChild('closeBtn') closeBtn;

  ngOnInit() {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/home']);
      console.log('User already logged in');
      return;
    }
    console.log('Modal login form opening...');
    this.showLoginForm();
  }

  showLoginForm() {
    this.loginModal.nativeElement.dispatchEvent(this.event);
  }

  closeModalForm() {
    this.closeBtn.nativeElement.dispatchEvent(this.event);
    this.router.navigate(['/home']);
  }

  onSubmit(form: NgForm) {
    console.log(form.value);
    this.userService.loginUser(form.value)
      .subscribe(
      (response: any) => {
        this.authService.login(response.token, response.email, this.infra.DEFAULT_PHOTO_URL);
        this.closeModalForm()
      },
      (errorResponse: HttpErrorResponse) => {
        this.errors = errorResponse.error.errors;
        console.log(errorResponse);
      }
    );
  }

  googleLogin() {
    this.userService.doGoogleLogin()
      .then(
      (response: any) => {        
        console.log(response);
        this.authService.login(response.credential.idToken, response.user.email, response.user.photoURL);
        this.closeModalForm()
      },
      (errorResponse: HttpErrorResponse) => {
        this.errors = errorResponse.error;
        console.log(errorResponse);
      }
    );    
  }

  facebookLogin() {
    this.userService.doFacebookLogin()
      .then(
      (response: any) => {        
        console.log(response);
        this.authService.login(response.credential.accessToken, response.additionalUserInfo.profile.email, response.user.photoURL);
        this.closeModalForm()
      },
      (errorResponse: HttpErrorResponse) => {
        this.errors = new Array(errorResponse.message);
        console.log(errorResponse);
      }
    ); 
  }

  twitterLogin() {
    this.userService.doTwitterLogin()
      .then(
      (response: any) => {        
        console.log(response);
        this.authService.login(response.credential.accessToken, response.additionalUserInfo.username, response.user.photoURL);
        this.closeModalForm()
      },
      (errorResponse: HttpErrorResponse) => {
        this.errors = new Array(errorResponse.message);
        console.log(errorResponse);
      }
    ); 
  }
}
