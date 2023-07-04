import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-forum-card-page',
  templateUrl: './forum-card-page.component.html',
  styleUrls: ['./forum-card-page.component.css']
})
export class ForumCardPageComponent {
  @Input() title: string = '';
  @Input() description: string = '';
  @Input() userName: string = '';

}
