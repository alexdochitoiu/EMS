import { NgForm } from '@angular/forms/src/directives';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Component, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { UserRegisterModel } from '../../../services/user/user.model';
import { UserService } from '../../../services/user/user.service';
import { AuthService } from '../../../services/auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements AfterViewInit, OnDestroy {

  user: UserRegisterModel;
  errors: Array<string>;
  successRegistered = false;

  @ViewChild('content') content;
  private modalRef: NgbModalRef;

  constructor(private userService: UserService,
              private authService: AuthService,
              private modalService: NgbModal,
              private router: Router) {
    if (this.authService.isLoggedIn()) {
      this.authService.logout();
    }
    this.user = new UserRegisterModel();
  }

  ngAfterViewInit() {
    console.log('Modal register form opening...');
    this.open(this.content);
    this.resetForm();
  }

  ngOnDestroy() {
    if (this.modalRef !== null) {
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

  removeError(error) {
    this.errors = this.errors.filter(e => e !== error);
  }

  onSubmit(form: NgForm) {
    navigator.geolocation.getCurrentPosition(pos => {
      const crd = pos.coords;
      const registerModel = {
        ...form.value,
        currentLongitude: String(crd.longitude),
        currentLatitude: String(crd.latitude)
      };
      console.log(registerModel);
      this.userService.registerUser(registerModel)
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
          console.log(errorResponse);
        }
      );
    });
  }
}
