import { Component, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/services/user/user.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements AfterViewInit, OnDestroy {

  @ViewChild('content') content;
  private modalRef: NgbModalRef;

  constructor(private modalService: NgbModal,
    private userService: UserService) {}

  ngAfterViewInit() {
    this.open(this.content);
  }

  ngOnDestroy() {
    if (this.modalRef) {
      this.modalRef.close();
    }
  }

  open(content) {
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

  onSubmit(form: NgForm) {
    this.userService.forgotPassword(form.value).subscribe(
      (response: any) => {
        console.log(response);
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(errorResponse);
      }
    );
  }
}
