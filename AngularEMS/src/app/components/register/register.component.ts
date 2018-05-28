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

  constructor(private userService: UserService) {
      this.user = new User();
  }

  @ViewChild('register') registerModal;
  ngOnInit() {
    console.log('Modal register form opening...');
    this.showRegisterForm();
    this.resetForm();
  }

  showRegisterForm() {
    event = new MouseEvent('click', {bubbles: true});
    this.registerModal.nativeElement.dispatchEvent(event);
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.reset();
    }
    this.user.Username = '';
    this.user.Email = '';
    this.user.Password = '';
  }

  OnSubmit(form: NgForm) {
    this.userService.registerUser(form.value)
      .subscribe((data: any) => {
        if (data.Succeeded === true) {
          this.resetForm(form);
          alert('User registration successful');
        } else {
          console.log(data);
        }
      });
  }
}
