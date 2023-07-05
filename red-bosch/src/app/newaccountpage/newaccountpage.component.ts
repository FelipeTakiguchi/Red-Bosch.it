import { Component, Input, Output, EventEmitter } from '@angular/core';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-newaccountpage',
  templateUrl: './newaccountpage.component.html',
  styleUrls: ['./newaccountpage.component.css']
})

export class NewaccountpageComponent {
  @Output() valueChanged = new EventEmitter<string>();
  @Input() breakLineOnInput = true;
  @Input() canSeePassword = true;
  @Input() seePassword = false
  @Output() seePasswordChanged = new EventEmitter<boolean>();
  @Input() Nome: string = '';
  @Input() Email: string = '';
  @Output() Senha: string = '';
  @Output() ConfirmaSenha: string = '';
  @Output() DataNascimento!: Date;
  @Input()
  Foto!: string;
  maxDate: Date = new Date();
  fileToUpload: File | undefined;
  formData = new FormData();

  protected onHandleUpload(event: any) {
    this.formData = event
  }

  constructor(private userService: UserService, private router: Router) { }

  onRegister() {
    if (this.Nome != '' && this.Email != '' && this.Senha != '' && this.ConfirmaSenha != ''
      && this.DataNascimento != undefined && this.formData.get("file") != null) {
      if (this.Senha === this.ConfirmaSenha) {
        this.formData.delete('nome')
        this.formData.delete('email')
        this.formData.delete('senha')
        this.formData.delete('dataNascimento')
        this.formData.append('nome', this.Nome)
        this.formData.append('email', this.Email)
        this.formData.append('senha', this.Senha)
        this.formData.append('dataNascimento', this.DataNascimento.toLocaleDateString())

        this.userService.register(this.formData)
          .subscribe(res => {
            var body: any = res.status
            if (body == 200)
              this.router.navigate(["/login"])
            else
              window.alert("Credenciais incorretas!")
          })
      } else
        window.alert("As senhas não conferem!")
    } else
      window.alert("Preencha todos os campos!")
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