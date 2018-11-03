import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements AfterViewInit {
  
  constructor(private modalService: NgbModal) {}

  @ViewChild('content') content;
  private modalRef: NgbModalRef;

  ngAfterViewInit() {
    setTimeout(() => { this.open(this.content); });
  }

  ngOnDestroy() {
    if (this.modalRef !== null) this.modalRef.close()
  }

  open(content) {
    this.modalRef = this.modalService.open(content);
    this.modalRef.result.then(
      () => {
        console.log('When user closes'); 
      }, 
      () => {         
        console.log('Backdrop click')
      }
    );
  }
}
