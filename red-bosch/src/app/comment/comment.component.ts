import { Component, Input, OnInit } from '@angular/core';
import { Jwt } from 'src/DTO/Jwt';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input() content: string = "";
  @Input() dataPublicacao: Date = new Date();
  @Input() imageUsuario: string = '';
  @Input() nomeUsuario: string = '';
  @Input() usuario: string = '';
  @Input() isLogged: boolean = false;
  @Input() isDelete: boolean = false;


  constructor(private userService: UserService) {}

  jwt: Jwt = {
    jwt: '',
  }
  
  ngOnInit(): void {
    if (sessionStorage.getItem("jwtSession") != null) {
      this.jwt.jwt = sessionStorage.getItem("jwtSession")!
      this.isLogged = true;
    }
    else
      this.isLogged = false;

    if (this.jwt.jwt == this.usuario)
      this.isDelete = true;

    this.jwt.jwt = this.usuario;
    this.userService.getUser(this.jwt)
      .subscribe(res => {
        if (res.status == 200) {
          if (res.body?.nome != undefined)
            this.nomeUsuario = res.body.nome;
          if (res.body?.imageId.toString() != undefined)
            this.imageUsuario = "http://localhost:5022/img/" + (res.body?.imageId.toString())
        }
      })
  }
}
