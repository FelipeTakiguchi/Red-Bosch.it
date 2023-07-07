import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ForumService } from '../services/forum.services';
import { ForumDTO } from 'src/DTO/ForumDTO';

@Component({
  selector: 'app-forum-card-page',
  templateUrl: './forum-card-page.component.html',
  styleUrls: ['./forum-card-page.component.css']
})
export class ForumCardPageComponent {
  @Input() id: number = 0;
  @Input() title: string = '';
  @Input() description: string = '';
  @Input() userName: string = '';
  @Input() image: string = '';
  @Input() inscritos: number = 0;
  @Input() enter: boolean = false;
  formData = new FormData();

  constructor(private router: Router, private forumService: ForumService) { }

  getForum() {
    this.formData.delete('idUsuario')
    this.formData.delete('idForum')
    this.formData.append('idUsuario', sessionStorage.getItem("jwtSession") ?? '')
    this.formData.append('idForum', this.id.toString())

    this.forumService.addUserForum(this.formData)
      .subscribe(res => {
        var body: any = res.status
        if (body == 200) {
          this.router.navigate(["/forumPage/" + this.id])
        }
      });
    this.router.navigate(["/forumPage/" + this.id.toString()])
  }
  
  enterForum() {
    this.router.navigate(["/forumPage/" + this.id.toString()])
  }
}
