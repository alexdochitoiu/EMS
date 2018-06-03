import { NgForm } from '@angular/forms/src/directives';
import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { UserRegisterModel } from '../../../services/user/user.model';
import { UserService } from '../../../services/user/user.service';
import { AuthService } from '../../../services/auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user: UserRegisterModel;
  event: MouseEvent;
  errors: Array<string>;
  successRegistered = false;

  constructor(private userService: UserService,
              private authService: AuthService) {
    if (this.authService.isLoggedIn()) {
      this.authService.logout();
    }
    this.user = new UserRegisterModel();
    this.event = new MouseEvent('click', {bubbles: true});
  }

  @ViewChild('register') registerModal;

  ngOnInit() {
    
    console.log('Modal register form opening...');
    this.showRegisterForm();
    this.resetForm();
  }

  showRegisterForm() {
    this.registerModal.nativeElement.dispatchEvent(this.event);
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.reset();
    }
    this.user.Username = '';
    this.user.Email = '';
    this.user.Password = '';
    this.user.ConfirmPassword = '';
    this.user.Phone = '';
    this.successRegistered = false;
    this.errors = null;
  }

  onSubmit(form: NgForm) {
    console.log(form.value);
    this.userService.registerUser(form.value)
      .subscribe(
      (response: any) => {
        console.log('Response:' + response);
        if (response.succeeded === true) {
          console.log(response.warnings);
          this.resetForm(form);
          this.successRegistered = true;
          console.log('User signed up with success!');
        }
      },
      (errorResponse: HttpErrorResponse) => {
        this.errors = errorResponse.error.errors;
        console.log(errorResponse.error.errors);
      }
    );
  }
}
