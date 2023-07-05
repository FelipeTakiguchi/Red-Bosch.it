import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

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

  constructor(private router: Router) { }

  getForum(){
    this.router.navigate(["/forumPage/" + this.id.toString()])
  }
}
