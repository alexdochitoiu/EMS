import { Component, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from 'src/app/services/user/user.service';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-verify-email',
  templateUrl: './verify-email.component.html',
  styleUrls: ['./verify-email.component.css']
})
export class VerifyEmailComponent implements AfterViewInit, OnDestroy {

  @ViewChild('content') content;
  private modalRef: NgbModalRef;
  message: string;
  success: boolean;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private modalService: NgbModal,
    private userService: UserService) { }

  ngAfterViewInit() {
    this.route.params.subscribe(params => {
      const userId = params['userId'];
      const token = encodeURIComponent(params['emailToken']);
      this.userService.verifyEmail(userId, token).subscribe(
        (response: any) => {
          this.success = true;
          this.message = response;
          this.open(this.content);
        },
        (errorResponse: HttpErrorResponse) => {
          this.success = false;
          this.message = errorResponse.status === 0
            ? 'Oops! Maybe the server is not open.'
            : errorResponse.error;
          console.log(errorResponse);
          this.open(this.content);
        }
      );
    });
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
        this.router.navigate(['/']);
      },
      () => {
        this.router.navigate(['/']);
      }
    );
  }
}
