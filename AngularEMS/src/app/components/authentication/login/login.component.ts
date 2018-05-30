import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from '@angular/forms/src/directives';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { UserLoginModel } from '../../../services/user/user.model';
import { UserService } from '../../../services/user/user.service';
import { AuthService } from '../../../services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  errors: Array<string>;
  user: UserLoginModel;

  constructor(private userService: UserService,
              private authService: AuthService,
              private router: Router) {
    this.user = new UserLoginModel();
  }

  @ViewChild('login') loginModal;
  ngOnInit() {
    console.log('Modal login form opening...');
    this.showLoginForm();
  }

  showLoginForm() {
    event = new MouseEvent('click', {bubbles: true});
    this.loginModal.nativeElement.dispatchEvent(event);
  }

  onSubmit(form: NgForm) {
    console.log(form.value);
    this.userService.loginUser(form.value)
      .subscribe(
      (response: any) => {
        this.authService.login(response.token);
        this.router.navigate(['/dashboard']);
      },
      (errorResponse: HttpErrorResponse) => {
        this.errors = errorResponse.error.errors;
        console.log(errorResponse.error.errors);
      }
    );
  }
}
