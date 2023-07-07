import { Component, Input, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  @Input() isLogged: boolean = false;
  title = 'red-bosch';

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.router.events.subscribe((val) => {
      let isNavEnd = val instanceof NavigationEnd;

      if (sessionStorage.getItem("jwtSession") != null)
        this.isLogged = true;
      else
        this.isLogged = false;
        
      if (!isNavEnd)
        return;
    })
      
    this.router.events.subscribe((val) => {
      let isNavEnd = val instanceof NavigationEnd;
      if (!isNavEnd)
        return;
      if (sessionStorage.getItem("jwtSession") != null && (location.pathname == "/login" || location.pathname == "/newaccount"))
        this.router.navigate(["/"]);
      else if(sessionStorage.getItem("jwtSession") == null && (location.pathname.includes("/forumPage") || location.pathname == "/createForumPage" || location.pathname == "/editProfile"))
        this.router.navigate(["/login"])
    })
  }
  
  LogOff() {
    sessionStorage.removeItem("jwtSession");
    this.router.navigate(["/login"]);
  }
}
