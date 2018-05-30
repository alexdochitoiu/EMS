import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  constructor() { }

  @ViewChild('forgotPassword') forgotPasswordModal;
  ngOnInit() {
    console.log('Modal login form opening...');
    this.showForgotPasswordForm();
  }

  showForgotPasswordForm() {
    event = new MouseEvent('click', {bubbles: true});
    this.forgotPasswordModal.nativeElement.dispatchEvent(event);
  }

}
