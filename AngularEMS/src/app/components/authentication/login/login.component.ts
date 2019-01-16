import { Component, AfterViewInit, ViewChild, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from '@angular/forms/src/directives';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { UserLoginModel, ExternalUserModel } from '../../../services/user/user.model';
import { UserService } from '../../../services/user/user.service';
import { AuthService } from '../../../services/auth/auth.service';
import { InfrastructureService } from '../../../services/infra/infra.service';
import { ngxLoadingAnimationTypes } from 'ngx-loading';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements AfterViewInit, OnDestroy {

  errors: Array<string>;
  user: UserLoginModel;
  verifyEmailIndex: number;
  verifyEmailSent: boolean;

  public ngxLoadingAnimationTypes = ngxLoadingAnimationTypes;
  public loading: boolean;

  constructor(private userService: UserService,
              private authService: AuthService,
              private router: Router,
              private infra: InfrastructureService,
              private modalService: NgbModal,
              private cd: ChangeDetectorRef) {
    this.user = new UserLoginModel();
    this.verifyEmailSent = false;
    this.loading = false;
  }

  @ViewChild('content') content: any;
  private modalRef: NgbModalRef;

  ngAfterViewInit() {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/']);
      console.log('User already logged in');
      return;
    }
    console.log('Modal login form opening...');
    setTimeout(() => this.open(this.content));
  }

  ngOnDestroy() {
    if (this.modalRef) {
      this.modalRef.close();
    }
  }

  open(content: any) {
    this.modalRef = this.modalService.open(content);
    this.modalRef.result.then(
      () => {
        console.log('When user closes');
      },
      () => {
        console.log('Backdrop click');
      }
    );
  }

  resendVerificationMail(form: NgForm) {
    this.userService.resendVerificationMail({ EmailOrUsername: form.value.EmailOrUsername })
      .subscribe(
      (response: any) => {
        console.log(response);
        this.errors = [];
        this.verifyEmailSent = true;
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(errorResponse);
        this.errors = errorResponse.error.errors;
      }
    );
  }

  onSubmit(form: NgForm) {
    this.loading = true;
    navigator.geolocation.getCurrentPosition(pos => {
      const crd = pos.coords;
      const loginModel = {
        ...form.value,
        currentLongitude: String(crd.longitude),
        currentLatitude: String(crd.latitude)
      };
      console.log(loginModel);
      this.userService.loginUser(loginModel)
        .subscribe(
        (response: any) => {
          this.authService.login(response.token, response.email, this.infra.DEFAULT_PHOTO_URL);
          this.loading = false;
          this.router.navigate(['/']);
        },
        (errorResponse: HttpErrorResponse) => {
          this.errors = errorResponse.error.errors;
          this.verifyEmailIndex = this.errors.indexOf('E-mail was not confirmed');
          console.log(errorResponse);
          this.loading = false;
        }
      );
    });
  }

  googleLogin() {
    this.loading = true;
    this.userService.doGoogleLogin()
      .then(
      (response: any) => {
        navigator.geolocation.getCurrentPosition(pos => {
          const crd = pos.coords;
          const externalUser: ExternalUserModel = {
            Provider: 'Google',
            FirstName: response.additionalUserInfo.profile.given_name,
            LastName: response.additionalUserInfo.profile.family_name,
            Email: response.additionalUserInfo.profile.email,
            PhotoUrl: response.additionalUserInfo.profile.picture,
            Token: response.credential.accessToken,
            CurrentLongitude: String(crd.longitude),
            CurrentLatitude: String(crd.latitude)
          };
          this.userService.registerExternalUser(externalUser).toPromise().then(
            (res) => {
              console.log(res);
              this.authService.login(
                response.credential.accessToken,
                response.additionalUserInfo.profile.email,
                response.additionalUserInfo.profile.picture
              );
              this.loading = false;
              this.router.navigate(['/']);
            },
            (err: HttpErrorResponse) => {
              this.errors = err.error.errors;
              this.loading = false;
            }
          );
          console.log(externalUser);
        });
      },
      (errorResponse: HttpErrorResponse) => {
        this.errors = errorResponse.error;
        console.log(errorResponse);
        this.loading = false;
      }
    );
  }

  facebookLogin() {
    this.loading = true;
    this.userService.doFacebookLogin()
      .then(
      (response: any) => {
        navigator.geolocation.getCurrentPosition(pos => {
          const crd = pos.coords;
          const externalUser: ExternalUserModel = {
            Provider: 'Facebook',
            FirstName: response.additionalUserInfo.profile.first_name,
            LastName: response.additionalUserInfo.profile.last_name,
            Email: response.additionalUserInfo.profile.email,
            PhotoUrl: response.additionalUserInfo.profile.picture.data.url,
            Token: response.credential.accessToken,
            CurrentLongitude: String(crd.longitude),
            CurrentLatitude: String(crd.latitude)
          };
          this.userService.registerExternalUser(externalUser).toPromise().then(
            (res) => {
              console.log(res);
              this.authService.login(response.credential.accessToken, response.additionalUserInfo.profile.email, response.user.photoURL);
              this.loading = false;
              this.router.navigate(['/']);
            },
            (err: HttpErrorResponse) => {
              this.errors = err.error.errors;
              this.loading = false;
            }
          );
          console.log(externalUser);
        });
      },
      (errorResponse: HttpErrorResponse) => {
        this.errors = new Array(errorResponse.message);
        console.log(errorResponse);
        this.loading = false;
      }
    );
  }

  twitterLogin() {
    this.loading = true;
    this.userService.doTwitterLogin()
      .then(
      (response: any) => {
        console.log(response);
        this.loading = false;
        this.authService.login(response.credential.accessToken, response.additionalUserInfo.username, response.user.photoURL);
        this.router.navigate(['/']);
      },
      (errorResponse: HttpErrorResponse) => {
        this.errors = new Array(errorResponse.message);
        this.loading = false;
        console.log(errorResponse);
      }
    );
  }

  removeError(error: string) {
    this.errors = this.errors.filter(e => e !== error);
  }
}
