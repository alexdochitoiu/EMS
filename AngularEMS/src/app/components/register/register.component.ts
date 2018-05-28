import { Router } from '@angular/router';
import { NgForm } from '@angular/forms/src/directives';
import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../services/user/user.model';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user: User;
  event: MouseEvent;
  errors: Array<string>;
  successRegistered = false;

  constructor(private userService: UserService,
              private router: Router) {
      this.user = new User();
      event = new MouseEvent('click', {bubbles: true});
  }

  @ViewChild('register') registerModal;
  @ViewChild('closeModal') closeModal;
  ngOnInit() {
    console.log('Modal register form opening...');
    this.showRegisterForm();
    this.resetForm();
  }

  showRegisterForm() {
    this.registerModal.nativeElement.dispatchEvent(event);
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.reset();
    }
    this.user.Username = '';
    this.user.Email = '';
    this.user.Password = '';
    this.user.ConfirmPassword = '';
    this.successRegistered = false;
    this.errors = null;
  }

  OnSubmit(form: NgForm) {
    this.userService.registerUser(form.value)
      .subscribe(
      (res) => {
        console.log('Response:' + res);
        if (res.succeeded === true) {
          this.resetForm(form);
          this.successRegistered = true;
          console.log('User signed up with success!');
        }
      },
      (err) => {
        this.errors = err.error.errors;
        console.log(err.error.errors);
      }
    );
  }
}
