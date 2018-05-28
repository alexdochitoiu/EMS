import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  @ViewChild('login') loginModal;
  ngOnInit() {
    console.log('Modal login form opening...');
    this.showLoginForm();
  }

  showLoginForm() {
    event = new MouseEvent('click', {bubbles: true});
    this.loginModal.nativeElement.dispatchEvent(event);
  }
}
