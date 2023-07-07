import { Component, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { LoginDTO } from 'src/DTO/LoginDTO';

@Component({
  selector: 'app-loginpage',
  templateUrl: './loginpage.component.html',
  styleUrls: ['./loginpage.component.css']
})

export class LoginpageComponent {
  @Input() Email: string = '';
  @Output() Senha: string = '';

  constructor(private userService: UserService, private router: Router) { }

  userLogin: LoginDTO = {
    Email: '',
    Senha: '',
  };

  onLogin() {
    if (this.Email != '' && this.Senha != '') {
      this.userLogin.Email = this.Email;
      this.userLogin.Senha = this.Senha;

      this.userService.login(this.userLogin)
        .subscribe(res => {
          var body: any = res.body
          if (body.success) {
            sessionStorage.setItem("jwtSession", body.jwt)
            this.router.navigate(["/"])
          } else
            window.alert("Credenciais incorretas!")
        })
    }
    else
      window.alert("Preencha todos os campos!")
  }

  protected passwordChanged(event: any) {
    this.Senha = event;
  }
}
