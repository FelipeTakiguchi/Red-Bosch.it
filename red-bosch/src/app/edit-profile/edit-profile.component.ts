import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserService } from '../services/user.service';
import { Jwt } from 'src/DTO/Jwt';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent {
  @Output() valueChanged = new EventEmitter<string>();
  @Input() Nome: string | undefined;
  @Input() Imagem: File | undefined;
  @Input() url: string | undefined;
  @Input() Descricao: string | undefined;
  
  formData = new FormData();

  protected onHandleUpload(event: any) {
    this.formData = event
  }

  text = "Altere aqui..."
  savedText = ""

  constructor(private userService: UserService, private router: Router) { }

  jwt: Jwt = {
    jwt: '',
  }

  ngOnInit(): void {
    if (sessionStorage.getItem("jwtSession") != null)
      this.jwt.jwt = sessionStorage.getItem("jwtSession")!

    this.userService.getUser(this.jwt)
      .subscribe(res => {
        if (res.status == 200) {
          this.Nome = res.body?.nome;
          this.Descricao = res.body?.descricao;
          if (res.body?.imageId.toString() != undefined)
            this.url = "http://localhost:5022/img/" + (res.body?.imageId.toString())
        }
      })
  }

  updateUser() {
    if (this.Nome != undefined && this.Descricao != undefined) {
      this.formData.delete('idUser')
      this.formData.delete('nome')
      this.formData.delete('descricao')
      this.formData.append('idUser', sessionStorage.getItem('jwtSession') ?? '')
      this.formData.append('nome', this.Nome)
      this.formData.append('descricao', this.Descricao)

      this.userService.updateUser(this.formData)
        .subscribe(res => {
          var body: any = res.status
          if (body == 200)
            this.router.navigate(["/"])
          else
            window.alert("Ocorreu um erro ao atualizar seu perfil!")
        })
    }
  }
}
