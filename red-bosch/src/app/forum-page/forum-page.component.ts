import { Component, Input } from '@angular/core';
import { UserService } from '../services/user.service';
import { Jwt } from 'src/DTO/Jwt';
import { ForumService } from '../services/forum.services';
import { ForumDTO } from 'src/DTO/ForumDTO';

@Component({
  selector: 'app-forum-page',
  templateUrl: './forum-page.component.html',
  styleUrls: ['./forum-page.component.css']
})

export class ForumPageComponent {
  forums: ForumDTO[] = [];
  @Input() Nome: string | undefined;
  @Input() Descricao: string | undefined;
  @Input() url: string | undefined;
  @Input() isLogged: boolean = false;
  @Input() option: boolean = false;
  formData = new FormData();

  constructor(private userService: UserService, private forumService: ForumService) { }

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

    this.userService.getUser(this.jwt)
      .subscribe(res => {
        if (res.status == 200) {
          this.Nome = res.body?.nome;
          this.Descricao = res.body?.descricao;
          if (res.body?.imageId.toString() != undefined)
            this.url = "http://localhost:5022/img/" + (res.body?.imageId.toString())
        }
      })

    this.showAll()
  }

  showJoined() {
    this.forumService.getJoinedForums(this.jwt.jwt)
      .subscribe(list => {
        var newList: ForumDTO[] = []
        list.forEach(forum => {
          newList.push({
            id: forum.id,
            titulo: forum.titulo,
            descricao: forum.descricao,
            IdUsuario: forum.IdUsuario,
            inscritos: forum.inscritos,
            imageId: "http://localhost:5022/img/" + forum.imageId,
          })
        });

        this.forums = newList
        this.option = true
      })
  }

  showAll() {
    this.forumService.getSomeForum(this.jwt.jwt)
      .subscribe(list => {
        var newList: ForumDTO[] = []
        list.forEach(forum => {
          newList.push({
            id: forum.id,
            titulo: forum.titulo,
            descricao: forum.descricao,
            IdUsuario: forum.IdUsuario,
            inscritos: forum.inscritos,
            imageId: "http://localhost:5022/img/" + forum.imageId,
          })
        });

        this.forums = newList
        this.option = false
      })
  }
}
