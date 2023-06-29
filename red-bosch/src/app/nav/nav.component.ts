import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})

export class NavComponent{
  @Input() selected: number = 0;

  constructor(private router: Router) {
    router.events.subscribe((val) => {
      console.log(this.selected)
      console.log(this.router.url)
      if (this.router.url == "/") {
        this.selected = 1;
      } else if(this.router.url == "/forumPage"){
        this.selected = 2;
      } else if(this.router.url == "/login"){
        this.selected = 3;
      }
      console.log(this.router.url == "/forumPage")
    })
  }
}
