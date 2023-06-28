import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../user.service';
import { LoginDTO } from 'src/DTO/LoginDTO';
import { RegisterDTO } from 'src/DTO/RegisterDTO';

@Component({
  selector: 'app-loginpage',
  templateUrl: './loginpage.component.html',
  styleUrls: ['./loginpage.component.css']
})

export class LoginpageComponent implements OnInit {
  protected isLogin = true;
  @Output() public onChangeFormClick = new EventEmitter<any>();

  ngOnInit(): void {
    console.log((this.router.url) == "/login/newaccount")
    if(this.router.url != "/login/newaccount")
      this.isLogin = true;
    else
      this.isLogin = false;
  }

  constructor(private userService: UserService, private router: Router) { }

  userLogin: LoginDTO = {
    Email: '',
    Senha: '',
  };

  onLogin() {
    this.userService.login(this.userLogin)
      .subscribe(res => {
        var body: any = res.body
        if (body.success) {
          sessionStorage.setItem("jwtSession", body.jwt)
          this.router.navigate(["/"])
        }
      })
  }
  
  userRegister: RegisterDTO = {
    Email: '',
    Senha: '',
    Nome: '',
    Datanascimento: new Date()
  };

  onRegister() {
    this.userService.register(this.userRegister)
      .subscribe(res => {
        var body: any = res.body
        console.log(body)
        if (body.success) {
          sessionStorage.setItem("jwtSession", body.jwt)
          this.router.navigate(["/"])
        }
      })
  }
}
