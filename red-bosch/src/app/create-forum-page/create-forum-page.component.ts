import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ForumService } from '../services/forum.services';

@Component({
  selector: 'app-create-forum-page',
  templateUrl: './create-forum-page.component.html',
  styleUrls: ['./create-forum-page.component.css']
})

export class CreateForumPageComponent {
  @Input()
  Nome!: string;
  @Input()
  Descricao!: string;
  @Input()
  Foto!: string;
  fileToUpload: File | undefined;
  formData = new FormData();

  protected onHandleUpload(event: any) {
    this.formData = event
  }

  constructor(private forumService: ForumService, private router: Router) { }

  CreateForum() {
    if (this.Nome && this.Descricao && this.formData.get("file") != null) {
      this.formData.delete('titulo')
      this.formData.delete('descricao')
      this.formData.delete('idUsuario')
      this.formData.append('titulo', this.Nome)
      this.formData.append('descricao', this.Descricao)
      this.formData.append('idUsuario', sessionStorage.getItem("jwtSession") ?? '')

      this.forumService.register(this.formData)
        .subscribe(res => {
          var body: any = res.status
          if (body == 200) {
            this.router.navigate(["/forumPage"])
          }
        })
    } else
      window.alert("Preencha todos os campos!")
  }
}
