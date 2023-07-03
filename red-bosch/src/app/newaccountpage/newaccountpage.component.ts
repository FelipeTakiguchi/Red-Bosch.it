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
  fileToUpload: File | undefined;
  formData = new FormData();

  protected onHandleUpload(event : any) {
    this.formData = event
  }

  constructor(private userService: UserService, private router: Router) { }
   
  onRegister() {    
    if (this.Senha === this.ConfirmaSenha) {
      const formData = new FormData();
      formData.append('nome', this.Nome)
      formData.append('email', this.Email)
      formData.append('senha', this.Senha)
      formData.append('dataNascimento', this.DataNascimento.toString())      
      console.log(formData.get('nome'))
      this.userService.register(formData)
        .subscribe(res => {
          var body: any = res.status
          console.log(res)
          if (body == 200) {
            sessionStorage.setItem("jwtSession", body.jwt)
            this.router.navigate(["/login"])
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