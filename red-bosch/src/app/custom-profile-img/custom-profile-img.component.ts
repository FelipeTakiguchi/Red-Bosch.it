import { Component, Input } from '@angular/core';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-custom-profile-img',
  templateUrl: './custom-profile-img.component.html',
  styleUrls: ['./custom-profile-img.component.css']
})
export class CustomProfileImgComponent {
  @Input() url: string | undefined;
  @Input() size: string | undefined;
  @Input() inputStyle: string | undefined;
  @Input() Nome: string | undefined = "";

}