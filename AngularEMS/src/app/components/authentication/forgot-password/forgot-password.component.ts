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
  private hidden: boolean;
  private success: boolean;
  private message: string;

  constructor(private modalService: NgbModal,
    private userService: UserService) {
    this.hidden = true;
  }

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
        this.modalRef.close();
        this.success = true;
        this.message = 'A password reset link was sent on your e-mail!';
        this.hidden = false;
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(errorResponse);
        this.modalRef.close();
        this.success = false;
        this.message = 'Something went wrong. \nDescription: ' +
          errorResponse.error.errors.map(x => x.description).join('.\n');
          this.hidden = false;
      }
    );
  }
}
