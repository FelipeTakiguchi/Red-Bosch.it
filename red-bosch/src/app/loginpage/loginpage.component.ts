import { Component, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../user.service';
import { LoginDTO } from 'src/DTO/LoginDTO';
import { RegisterDTO } from 'src/DTO/RegisterDTO';

@Component({
  selector: 'app-loginpage',
  templateUrl: './loginpage.component.html',
  styleUrls: ['./loginpage.component.css']
})

export class LoginpageComponent {
  @Input()
  Email!: string;
  @Output()
  Senha!: string;

  constructor(private userService: UserService, private router: Router) { }

  userLogin: LoginDTO = {
    Email: '',
    Senha: '',
  };

  onLogin() {
    this.userLogin.Email = this.Email;
    this.userLogin.Senha = this.Senha;
    console.log(this.userLogin)
    this.userService.login(this.userLogin)
      .subscribe(res => {
        var body: any = res.body
        console.log(res)
        if (body.success) {
          sessionStorage.setItem("jwtSession", body.jwt)
          this.router.navigate(["/"])
        }
      })
  }

  protected passwordChanged(event: any) {
    this.Senha = event;
  }
}
