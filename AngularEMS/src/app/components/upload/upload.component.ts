import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { DropzoneComponent , DropzoneDirective, DropzoneConfigInterface } from 'ngx-dropzone-wrapper';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {

  public config: DropzoneConfigInterface = {
    clickable: true,
    maxFiles: 6,
    addRemoveLinks: true
  };

  @Output() fileEvent = new EventEmitter<File>();
  errorMessage: string;

  constructor() { }

  ngOnInit() {
  }

  public onUploadSuccess(args: any): void {
    this.fileEvent.emit(args[0]);
  }

  public onUploadError(args: any): void {
    this.errorMessage = String(args[1]);
  }
}
