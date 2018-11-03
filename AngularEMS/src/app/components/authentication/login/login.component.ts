import { Component, AfterViewInit, ViewChild } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from '@angular/forms/src/directives';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { UserLoginModel } from '../../../services/user/user.model';
import { UserService } from '../../../services/user/user.service';
import { AuthService } from '../../../services/auth/auth.service';
import { InfrastructureService } from '../../../services/infra/infra.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements AfterViewInit  {

  errors: Array<string>;
  user: UserLoginModel;

  constructor(private userService: UserService,
              private authService: AuthService,
              private router: Router,
              private infra: InfrastructureService,
              private modalService: NgbModal) {
    this.user = new UserLoginModel();
  }

  @ViewChild('content') content;
  private modalRef: NgbModalRef;

  ngAfterViewInit() {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/']);
      console.log('User already logged in');
      return;
    }
    console.log('Modal login form opening...');
    setTimeout(() => { this.open(this.content); });
  }

  ngOnDestroy() {
    if (this.modalRef) this.modalRef.close();
  }

  open(content) {
    this.modalRef = this.modalService.open(content);
    this.modalRef.result.then(
      () => {
        console.log('When user closes'); 
      }, 
      () => {         
        console.log('Backdrop click')
      }
    );
  }

  onSubmit(form: NgForm) {
    console.log(form.value);
    this.userService.loginUser(form.value)
      .subscribe(
      (response: any) => {
        this.authService.login(response.token, response.email, this.infra.DEFAULT_PHOTO_URL);
        this.router.navigate(['/']);
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
        this.router.navigate(['/']);
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
        this.router.navigate(['/']);
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
        this.router.navigate(['/']);
      },
      (errorResponse: HttpErrorResponse) => {
        this.errors = new Array(errorResponse.message);
        console.log(errorResponse);
      }
    ); 
  }
}
