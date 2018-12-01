import { Component, OnDestroy, ViewChild, AfterViewInit } from '@angular/core';
import { Input } from '@angular/core';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-popup-message',
  templateUrl: './popup-message.component.html',
  styleUrls: ['./popup-message.component.css']
})
export class PopupMessageComponent implements OnDestroy, AfterViewInit {

  @ViewChild('content') content;
  @Input() success: boolean;
  @Input() heading: string;
  @Input() message: string;
  private modalRef: NgbModalRef;

  constructor(private router: Router,
    private modalService: NgbModal) {
      this.message = 'Content';
      this.heading = 'Heading';
    }

  ngAfterViewInit(): void {
    setTimeout(() => this.open(this.content));
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
