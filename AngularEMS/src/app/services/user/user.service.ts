import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { UserRegisterModel, UserLoginModel, UserModel, ExternalUserModel, ResetPasswordModel } from './user.model';
import { InfrastructureService } from '../infra/infra.service';
import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';

@Injectable({
  providedIn: 'root'
})
export class UserService {

   url: string;

  constructor(private http: HttpClient,
              private infra: InfrastructureService,
              private afAuth: AngularFireAuth) { }

  registerUser(user: UserRegisterModel) {
    this.url = this.infra.URL + '/api/account/register';
    return this.http.post(this.url, user).pipe(map((response: Response) => <any>response));
  }

  loginUser(user: UserLoginModel) {
    this.url = this.infra.URL + '/api/account/login';
    return this.http.post(this.url, user).pipe(map((response: Response) => <any>response));
  }

  verifyEmail(userId: string, emailToken: string) {
    this.url = this.infra.URL + '/api/account/verify/email/' + userId + '/' + emailToken;
    return this.http.get(this.url, { responseType: 'text'});
  }

  forgotPassword(model: any) {
    this.url = this.infra.URL + '/api/account/forgot-password';
    return this.http.post(this.url, model).pipe(map((response: Response) => <any>response));
  }

  resetPassword(userId: string, resetPasswordToken: string, resetPasswordModel: ResetPasswordModel) {
    this.url = this.infra.URL + '/api/account/reset/password/' + userId + '/' + resetPasswordToken;
    return this.http.post(this.url, resetPasswordModel).pipe(map((response: Response) => <any>response));
  }

  resendVerificationMail(model: any) {
    this.url = this.infra.URL + '/api/account/resend-verification-mail';
    return this.http.post(this.url, model).pipe(map((response: Response) => <any>response));
  }

  userByEmail(email: string) {
    this.url = this.infra.URL + '/api/users/' + email;
    return this.http.get(this.url);
  }

  userByUsername(username: string): Observable<UserModel> {
    this.url = this.infra.URL + '/api/users/by-username/' + username;
    return this.http.get(this.url)
      .pipe(map((u: any) =>
        new UserModel(
          u.firstName || 'Undefined',
          u.lastName || 'Undefined',
          u.username || 'Undefined',
          u.email || 'Undefined',
          u.gender || 'Undefined',
          u.dateOfBirth || 'Undefined',
          u.phoneNumber || 'Undefined',
          u.address || 'Undefined')
        )
      );
  }

  doFacebookLogin() {
    return new Promise<any>((resolve, reject) => {
      const provider = new firebase.auth.FacebookAuthProvider();
      this.afAuth.auth
      .signInWithPopup(provider)
      .then(res => {
        resolve(res);
      }, err => {
        reject(err);
      });
    });
  }

  doGoogleLogin() {
    return new Promise<any>((resolve, reject) => {
      const provider = new firebase.auth.GoogleAuthProvider();
      provider.addScope('profile');
      provider.addScope('email');
      this.afAuth.auth
      .signInWithPopup(provider)
      .then(res => {
        resolve(res);
      }, err => {
        reject(err);
      });
    });
  }

  doTwitterLogin() {
    return new Promise<any>((resolve, reject) => {
      const provider = new firebase.auth.TwitterAuthProvider();
      this.afAuth.auth
      .signInWithPopup(provider)
      .then(res => {
        resolve(res);
      }, err => {
        reject(err);
      });
    });
  }

  registerExternalUser(externalUser: ExternalUserModel) {
    this.url = this.infra.URL + '/api/account/login/external';
    return this.http.post(this.url, externalUser).pipe(map((response: Response) => <any>response));
  }
}
