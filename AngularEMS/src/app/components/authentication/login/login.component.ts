import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
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
  event: MouseEvent;

  constructor(private userService: UserService,
              private authService: AuthService,
              private router: Router) {
    this.user = new UserLoginModel();
    this.event = new MouseEvent('click', {bubbles: true});
  }

  @ViewChild('login') loginModal;
  @ViewChild('closeBtn') closeBtn;

  ngOnInit() {
    if (this.authService.isLogged()) {
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
        this.authService.login(response.token, response.email);
        this.closeModalForm()
      },
      (errorResponse: HttpErrorResponse) => {
        this.errors = errorResponse.error.errors;
        console.log(errorResponse);
      }
    );
  }
}
