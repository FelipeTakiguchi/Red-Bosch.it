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
  @Input() url: string | undefined;

  constructor(private userService: UserService, private forumService: ForumService) { }

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
          if(res.body?.imageId.toString() != undefined)
            this.url = "http://localhost:5022/img/" + (res.body?.imageId.toString())
        }
      })
      console.log(this.jwt)
      this.forumService.getAll()
      .subscribe(list => {
        console.log(list)
        var newList: ForumDTO[] = []
        list.forEach(forum => {
          newList.push({
            titulo: forum.titulo,
            descricao: forum.descricao,
            IdUsuario: forum.IdUsuario,
            imageId: "http://localhost:5022/img/" + forum.imageId,
          })
          console.log(newList);
        });

        this.forums = newList
      })
  }

}
