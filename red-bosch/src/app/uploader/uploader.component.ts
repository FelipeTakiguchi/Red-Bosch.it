import { Component, EventEmitter, OnInit, Output, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styleUrls: ['./uploader.component.css'],
})
export class UploaderComponent implements OnInit {
  @Output() onUploadFinished = new EventEmitter<any>();
  @Input() value: FormData = new FormData();
  @Input() size: string = '';
  @Input() foto: string | undefined = "";

  route = new Router();
  ngOnInit(): void { }

  imgUrl: string = '';

  uploadFile = (files: any) => {
    this.foto = "";
    if (files.length == 0) {
      return;
    }

    let fileToUpload = <File>files[0];

    this.value = new FormData();
    this.value.append('file', fileToUpload, fileToUpload.name);
    this.imgUrl = URL.createObjectURL(fileToUpload);
    this.onUploadFinished.emit(this.value);
  };

  getImgSrc() {
    if (this.imgUrl !== '') return this.imgUrl;

    return '../assets/images/camera.png';
  }
}