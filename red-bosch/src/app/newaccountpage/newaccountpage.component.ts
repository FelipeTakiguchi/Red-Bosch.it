import { Component, Input, Output, EventEmitter } from '@angular/core';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { RegisterDTO } from 'src/DTO/RegisterDTO';
import { LocationDTO } from 'src/DTO/LocationDTO';

@Component({
  selector: 'app-newaccountpage',
  templateUrl: './newaccountpage.component.html',
  styleUrls: ['./newaccountpage.component.css']
})

export class NewaccountpageComponent {
  // Inputs podem ser acessados de fora do componente como propriedades HTML
  // Outputs podem ser acessados de fora do componente como eventos no estilo onclick
  @Output() valueChanged = new EventEmitter<string>();
  @Input() breakLineOnInput = true;
  @Input() canSeePassword = true;
  @Input() seePassword = false
  @Output() seePasswordChanged = new EventEmitter<boolean>();
  @Input()
  Nome!: string;
  @Input()
  Email!: string;
  @Output()
  Senha!: string;
  @Output()
  ConfirmaSenha!: string;
  @Output()
  DataNascimento!: Date;
  @Input()
  Foto!: string;
  maxDate: Date = new Date();

  constructor(private userService: UserService, private router: Router) { }

  userRegister: RegisterDTO = {
    Nome: '',
    Senha: '',
    Email: '',
    DataNascimento: new Date(),
  };

  userPhoto: LocationDTO = {
    Nome: '',
    Photo: 0,
  };
   
  onRegister() {
    if (this.Senha === this.ConfirmaSenha) {
      this.userRegister.Nome = this.Nome;
      this.userRegister.Senha = this.Senha;
      this.userRegister.Email = this.Email;
      this.userRegister.DataNascimento = this.DataNascimento;
      this.userService.register(this.userRegister)
        .subscribe(res => {
          var body: any = res.status
          console.log(res)
          if (body == 200) {
            sessionStorage.setItem("jwtSession", body.jwt)
            this.router.navigate(["/"])
          }
        })
    }
  }

  protected passwordChanged1(event: any) {
    this.Senha = event;
  }

  protected passwordChanged2(event: any) {
    this.ConfirmaSenha = event;
  }

  protected checked = false;
  protected inputType = "password";
  protected inputStyle = "color: black;"
  protected inputText = "";
  protected initialState = true;

  protected showPassword() {
    if (this.checked)
      this.inputType = "text";
    else
      this.inputType = "password";

  }
}