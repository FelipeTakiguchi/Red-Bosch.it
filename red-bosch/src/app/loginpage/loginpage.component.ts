import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-loginpage',
  templateUrl: './loginpage.component.html',
  styleUrls: ['./loginpage.component.css']
})

export class LoginpageComponent {
  protected isLogin = true;

  protected changeTemplate(){
    this.isLogin = !this.isLogin;
  }
}
