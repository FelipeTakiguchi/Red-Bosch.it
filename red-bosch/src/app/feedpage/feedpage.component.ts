import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserService } from '../services/user.service';
import { Jwt } from 'src/DTO/Jwt';
import { PostDTO } from 'src/DTO/PostDTO';
import { PostService } from '../services/post.service';
import { ForumService } from '../services/forum.services';
import { UserForumDTO } from 'src/DTO/UserForumDTO';

@Component({
  selector: 'app-forum',
  templateUrl: './feedpage.component.html',
  styleUrls: ['./feedpage.component.css']
})

export class FeedPageComponent implements OnInit {
  @Input() Nome: string | undefined;
  @Input() Email: string | undefined;
  @Input() Descricao: string | undefined;
  @Input() Idade: number | undefined;
  @Input() Imagem: File | undefined;
  @Input() url: string | undefined;
  @Input() isLogged: boolean = false;
  @Input() participate: boolean = false;
  usersForums: UserForumDTO[] = []

  posts: PostDTO[] = []

  text = "Altere aqui..."
  savedText = ""

  constructor(private userService: UserService, private postService: PostService, private forumService: ForumService) { }

  jwt: Jwt = {
    jwt: '',
  }

  onPost() {
    window.location.reload()
  }

  ngOnInit(): void {
    if (sessionStorage.getItem("jwtSession") != null) {
      this.jwt.jwt = sessionStorage.getItem("jwtSession")!
      this.isLogged = true;
    }
    else
      this.isLogged = false;

    if (sessionStorage.getItem("jwtSession") != null)
      this.userService.getUser(this.jwt)
        .subscribe(res => {
          if (res.status == 200) {
            this.Nome = res.body?.nome;
            this.Email = res.body?.email;
            this.Descricao = res.body?.descricao;
            const Nascimento = new Date(new String(res.body?.dataNascimento).toString());
            let timeDiff = Math.abs(Date.now() - Nascimento.getTime());
            this.Idade = Math.floor((timeDiff / (1000 * 3600 * 24)) / 365.25);
            if (res.body?.imageId.toString() != undefined)
              this.url = "http://localhost:5022/img/" + (res.body?.imageId.toString())
          }
        })

    this.refreshPosts();
  }

  onClick() {
    this.savedText = this.text
  }
  update(event: any) {
    this.text = event.target.value
  }

  refreshPosts() {
    this.forumService.getAllUsersForum()
      .subscribe(ufs => {
        var newList: UserForumDTO[] = []
        ufs.forEach(uf => {
          newList.push({
            id: uf.id,
            idForum: uf.idForum,
            usuario: uf.usuario,
          })
        })

        this.usersForums = newList
      });

      this.postService.getAll()
      .subscribe(list => {
        var newList: PostDTO[] = []
        list.forEach(post => {
          var flag = false
          console.log(post)
          this.usersForums.forEach(uf => {
            if(uf.idForum == post.idForum && uf.usuario == sessionStorage.getItem("jwtSession"))
              flag = true
          })
          
          newList.push({
            id: post.id,
            conteudo: post.conteudo,
            dataPublicacao: post.dataPublicacao,
            imageId: post.imageId,
            idForum: post.idForum,
            jwt: post.jwt,
            participate: flag
          })
        });
        this.posts = newList
      })
  }
}