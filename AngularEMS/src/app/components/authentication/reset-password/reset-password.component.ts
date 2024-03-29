import { Component, AfterViewInit, OnDestroy, ViewChild } from '@angular/core';
import { UserService } from 'src/app/services/user/user.service';
import { ActivatedRoute } from '@angular/router';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { NgForm } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { ngxLoadingAnimationTypes } from 'ngx-loading';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements AfterViewInit, OnDestroy {

  @ViewChild('content') content;
  private modalRef: NgbModalRef;
  private hidden: boolean;
  private success: boolean;
  private message: string;

  public ngxLoadingAnimationTypes = ngxLoadingAnimationTypes;
  public loading: boolean;

  constructor(private userService: UserService,
              private route: ActivatedRoute,
              private modalService: NgbModal) {
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
    this.route.params.subscribe(params => {
      const userId = params['userId'];
      const token = encodeURIComponent(params['resetPasswordToken']);
      this.loading = true;
      this.userService.resetPassword(userId, token, form.value).subscribe(
        (response: any) => {
          console.log(response);
          this.modalRef.close();
          this.success = true;
          this.message = 'The password was successfully changed!';
          this.hidden = this.loading = false;
        },
        (errorResponse: HttpErrorResponse) => {
          console.log(errorResponse);
          this.modalRef.close();
          this.success = false;
          this.message = 'Something went wrong. \nDescription: ' +
            errorResponse.error.errors.map((x: { description: any; }) => x.description).join('.\n');
          this.hidden = this.loading = false;
        }
      );
    });
  }
}
