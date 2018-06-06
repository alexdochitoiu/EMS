import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { UserRegisterModel, UserLoginModel, UserModel } from './user.model';
import { InfrastructureService } from '../infra/infra.service';
import 'rxjs/add/operator/map';
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
    return this.http.post(this.url, user).map((response: Response) => <any>response);
  }
  
  loginUser(user: UserLoginModel) {
    this.url = this.infra.URL + '/api/account/login';
    return this.http.post(this.url, user).map((response: Response) => <any>response);
  }

  userByEmail(email: string) {
    this.url = this.infra.URL + '/api/users/' + email;
    return this.http.get(this.url);
  }

  userByUsername(username: string): Observable<UserModel> {
    this.url = this.infra.URL + '/api/users/' + username;
    return this.http.get(this.url)
      .map((u: any) => 
        new UserModel(u.firstName, u.lastName, u.username, u.email, 
          u.gender, u.dateOfBirth, u.phoneNumber, u.address));
  }
  
  doFacebookLogin(){
    return new Promise<any>((resolve, reject) => {
      let provider = new firebase.auth.FacebookAuthProvider();
      this.afAuth.auth
      .signInWithPopup(provider)
      .then(res => {
        resolve(res);
      }, err => {
        reject(err);
      })
    });
  }

  doGoogleLogin() {
    return new Promise<any>((resolve, reject) => {
      let provider = new firebase.auth.GoogleAuthProvider();
      provider.addScope('profile');
      provider.addScope('email');
      this.afAuth.auth
      .signInWithPopup(provider)
      .then(res => {
        resolve(res);
      }, err => {;
        reject(err);
      })
    });
  }

  doTwitterLogin() {
    return new Promise<any>((resolve, reject) => {
      let provider = new firebase.auth.TwitterAuthProvider();
      this.afAuth.auth
      .signInWithPopup(provider)
      .then(res => {
        resolve(res);
      }, err => {;
        reject(err);
      })
    });
  }
  
}
