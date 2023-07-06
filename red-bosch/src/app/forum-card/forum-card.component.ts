import { Component, Input, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { Jwt } from 'src/DTO/Jwt';

@Component({
  selector: 'app-forum-card',
  templateUrl: './forum-card.component.html',
  styleUrls: ['./forum-card.component.css']
})
export class ForumCardComponent implements OnInit{  
  @Input() conteudo: string = '';
  @Input() dataPublicacao?: Date;
  @Input() idUsuario: string = '';
  @Input() imageUsuario: string = '';
  @Input() nomeUsuario: string = '';
  @Input() imagemId: number = 0;
  @Input() isLogged: boolean = false;

  protected Upvoted = false;
  protected Downvoted = false;

  constructor(private userService: UserService) { }

  jwt: Jwt = {
    jwt: '',
  }
  
  ngOnInit(): void {
    if (sessionStorage.getItem("jwtSession") != null){
      this.jwt.jwt = sessionStorage.getItem("jwtSession")!
      this.isLogged = true;
    }
    else
      this.isLogged = false;
      
    this.jwt.jwt = this.idUsuario;
    this.userService.getUser(this.jwt)
      .subscribe(res => {
        console.log(res.body);
        if (res.status == 200) {
          if(res.body?.nome != undefined)
            this.nomeUsuario = res.body.nome;
          if(res.body?.imageId.toString() != undefined)
            this.imageUsuario = "http://localhost:5022/img/" + (res.body?.imageId.toString())
        }
      })
  }

  protected changeDownvote() {
    this.Upvoted = false;
    this.Downvoted = true;
  }

  protected changeUpvote() {
    this.Downvoted = false;
    this.Upvoted = true;
  }
}
