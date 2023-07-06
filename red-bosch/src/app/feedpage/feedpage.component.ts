import { Component, Input, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { Jwt } from 'src/DTO/Jwt';
import { PostDTO } from 'src/DTO/PostDTO';
import { PostService } from '../services/post.service';

@Component({
  selector: 'app-forum',
  templateUrl: './feedpage.component.html',
  styleUrls: ['./feedpage.component.css']
})

export class FeedPageComponent implements OnInit {
  @Input() Nome: string | undefined;
  @Input() Email: string | undefined;
  @Input() Idade: number | undefined;
  @Input() Imagem: File | undefined;
  @Input() url: string | undefined;
  @Input() isLogged: boolean = false;
  posts: PostDTO[] = []

  text = "Altere aqui..."
  savedText = ""

  constructor(private userService: UserService, private postService: PostService) { }

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

    this.userService.getUser(this.jwt)
      .subscribe(res => {
        if (res.status == 200) {
          this.Nome = res.body?.nome;
          this.Email = res.body?.email;
          const Nascimento = new Date(new String(res.body?.dataNascimento).toString());
          let timeDiff = Math.abs(Date.now() - Nascimento.getTime());
          this.Idade = Math.floor((timeDiff / (1000 * 3600 * 24))/365.25);
          if(res.body?.imageId.toString() != undefined)
            this.url = "http://localhost:5022/img/" + (res.body?.imageId.toString())
        }
      })

      this.postService.getAll()
      .subscribe(list => {
        console.log(list)
        var newList: PostDTO[] = []
        list.forEach(post => {
          newList.push({
            id: post.id,
            conteudo: post.conteudo,
            dataPublicacao: post.dataPublicacao,
            imageId: post.imageId,
            idForum: post.idForum,
            jwt: post.jwt
          })
          
          console.log(newList);
        });

        this.posts = newList
      })
  }

  onClick() {
    this.savedText = this.text
  }
  update(event: any) {
    this.text = event.target.value
  }
}