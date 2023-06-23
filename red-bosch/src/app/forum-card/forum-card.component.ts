import { Component } from '@angular/core';

@Component({
  selector: 'app-forum-card',
  templateUrl: './forum-card.component.html',
  styleUrls: ['./forum-card.component.css']
})
export class ForumCardComponent {
  protected Upvoted = false;
  protected Downvoted = false;

  protected changeDownvote() {
    this.Upvoted = false;
    this.Downvoted = true;
  }

  protected changeUpvote() {
    this.Downvoted = false;
    this.Upvoted = true;
  }
}
