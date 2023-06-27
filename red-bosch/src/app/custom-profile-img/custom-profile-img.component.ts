import { HttpClient } from '@angular/common/http';
import { Component, Input } from '@angular/core';


@Component({
  selector: 'app-custom-profile-img',
  templateUrl: './custom-profile-img.component.html',
  styleUrls: ['./custom-profile-img.component.css']
})
export class CustomProfileImgComponent {
  @Input() url: string | undefined;
  @Input() inputStyle: string | undefined;
    fileName = '';

    constructor(private http: HttpClient) {}

    onFileSelected(event) {

        const file:File = event.target.files[0];

        if (file) {

            this.fileName = file.name;

            const formData = new FormData();

            formData.append("thumbnail", file);

            const upload$ = this.http.post("/api/thumbnail-upload", formData);

            upload$.subscribe();
        }
    }
}