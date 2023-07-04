import { Component, Input } from '@angular/core';
import { UserService } from '../services/user.service';
import { Jwt } from 'src/DTO/Jwt';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent {
  @Input() Nome: string | undefined;
  @Input() Imagem: File | undefined;
  @Input() url: string | undefined;
  @Input() Descricao: string | undefined;

  text = "Altere aqui..."
  savedText = ""

  constructor(private userService: UserService) { }

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
          if(res.body?.imageId.toString() != undefined)
            this.url = "http://localhost:5022/img/" + (res.body?.imageId.toString())
        }
      })
  }
}
