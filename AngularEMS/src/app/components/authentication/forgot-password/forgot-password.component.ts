import { Component, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/services/user/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ngxLoadingAnimationTypes } from 'ngx-loading';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements AfterViewInit, OnDestroy {

  @ViewChild('content') content: any;
  private modalRef: NgbModalRef;
  private hidden: boolean;
  private success: boolean;
  private message: string;
  public ngxLoadingAnimationTypes = ngxLoadingAnimationTypes;
  public loading: boolean;

  constructor(private modalService: NgbModal,
    private userService: UserService) {
    this.hidden = true;
    this.loading = false;
  }

  ngAfterViewInit() {
    this.open(this.content);
  }

  ngOnDestroy() {
    if (this.modalRef) {
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

  onSubmit(form: NgForm) {
    this.loading = true;
    this.userService.forgotPassword(form.value).subscribe(
      (response: any) => {
        console.log(response);
        this.modalRef.close();
        this.success = true;
        this.message = 'A password reset link was sent on your e-mail!';
        this.hidden = this.loading = false;
      },
      (errorResponse: HttpErrorResponse) => {
        console.log(errorResponse);
        this.modalRef.close();
        this.success = this.hidden = this.loading = false;
        this.message = 'Something went wrong. \nDescription: ' +
          errorResponse.error.errors.map((x: { description: any; }) => x.description).join('.\n');
      }
    );
  }
}
