import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'red-bosch';

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.router.events.subscribe((val) => {
      let isNavEnd = val instanceof NavigationEnd;
      if (!isNavEnd)
        return;
      if (sessionStorage.getItem("jwtSession") != null && (location.pathname == "/login" || location.pathname == "/newaccount"))
        this.router.navigate(["/"]);
      else if(sessionStorage.getItem("jwtSession") == null && (location.pathname == "/forumPage" || location.pathname == "/createForumPage"))
        this.router.navigate(["/"])
    })
  }
}
