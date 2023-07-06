import { Component, Input } from '@angular/core';
import { ForumDTO } from 'src/DTO/ForumDTO';
import { PostDTO } from 'src/DTO/PostDTO';
import { Router } from '@angular/router';
import { PostService } from '../services/post.service';
import { ForumService } from '../services/forum.services';

@Component({
  selector: 'app-forum-page-details',
  templateUrl: './forum-page-details.component.html',
  styleUrls: ['./forum-page-details.component.css']
})
export class ForumPageDetailsComponent {
  posts: PostDTO[] = []
  forum: ForumDTO = { id: 0, titulo: '', descricao: '', IdUsuario: 0, imageId: '', inscritos: 0 };
  @Input() Nome: string | undefined;
  @Input() url: string | undefined;
  @Input() Content: string = '';
  @Input() ContentPost: string = '';
  formData = new FormData();
  id: string = '';

  protected onHandleUpload(event: any) {
    console.log("Uploadei")
    this.formData = event
  }

  constructor(private router: Router, private postService: PostService, private forumService: ForumService) { }

  ngOnInit(): void {
    this.id = location.pathname.split("/")[2];
    
    this.forumService.getForum(this.id)
      .subscribe(forum => {
        console.log(forum)
        this.forum = {
          id: forum.id,
          titulo: forum.titulo,
          descricao: forum.descricao,
          IdUsuario: forum.IdUsuario,
          inscritos: forum.inscritos,
          imageId: "http://localhost:5022/img/" + forum.imageId,
        };
      });
    this.postService.getPosts(this.id)
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

  CreatePost() {
    if (this.ContentPost) {
      this.formData.delete('content')
      this.formData.delete('date')
      this.formData.delete('idUsuario')
      this.formData.delete('idForum')
      this.formData.append('content', this.ContentPost)
      this.formData.append('date', new Date().toLocaleDateString())
      this.formData.append('idUsuario', sessionStorage.getItem("jwtSession") ?? '')
      this.formData.append('idForum', this.id)

      this.postService.addPost(this.formData)
        .subscribe(res => {
          var body: any = res.status
          if (body == 200) {
            this.router.navigate(["/forumPage/" + this.id])
          }
        })
    } else
      window.alert("Preencha todos os campos!")
  }
}
