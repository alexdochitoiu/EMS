import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { User } from './user.model';
import { RootUrlService } from '../root-url/root-url.service';
import 'rxjs/add/operator/map';

@Injectable({
  providedIn: 'root'
})
export class UserService {

   url: string;

  constructor(private http: HttpClient,
              private rootUrl: RootUrlService) { }

  registerUser(user: User) {
    this.url = this.rootUrl.URL + '/api/account/register';
    const body: User = {
      FirstName: 'Alexandru',
      LastName: 'Dochitoiu',
      Username: user.Username,
      Email: user.Email,
      Password: user.Password,
      ConfirmPassword: user.ConfirmPassword,
      Gender: '0',
      DateOfBirth: '2018-05-28T00:20:10.360Z',
      Phone: '0741734289',
      Country: 'Romania',
      City: 'Bacau',
      Street: 'Calea Moldovei',
      Number: '192',
      ZipCode: '600352',
    };
    return this.http.post(this.url, body).map((response: Response) => <any>response);
  }
}
