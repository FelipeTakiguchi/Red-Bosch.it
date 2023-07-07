import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})

export class NavComponent implements OnInit, OnDestroy {
  @Input() selected: number = 0;
  @Input() isLogged: boolean = false;

  constructor(private router: Router) { }

  ngOnDestroy(): void {
  }

  ngOnInit(): void {
    this.router.events.subscribe((val) => {
      let isNavEnd = val instanceof NavigationEnd;

      if (sessionStorage.getItem("jwtSession") != null)
        this.isLogged = true;
      else
        this.isLogged = false;
        
      if (!isNavEnd)
        return;

      if (location.pathname == "/")
        this.selected = 1;
      else if (location.pathname == "/forumPage")
        this.selected = 2;
      else if (location.pathname == "/login")
        this.selected = 3;
    })
  }
}
