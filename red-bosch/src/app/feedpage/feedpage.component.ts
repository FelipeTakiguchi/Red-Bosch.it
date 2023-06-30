import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { Jwt } from 'src/DTO/Jwt';

@Component({
  selector: 'app-forum',
  templateUrl: './feedpage.component.html',
  styleUrls: ['./feedpage.component.css']
})

export class FeedPageComponent implements OnInit {
  text = "Altere aqui..."
  savedText = ""

  constructor(private userService: UserService) { }

  jwt: Jwt = {
    jwt: '',
  }

  ngOnInit(): void {
    if (sessionStorage.getItem("jwtSession") != null)
      this.jwt.jwt = sessionStorage.getItem("jwtSession")!

    console.log(this.jwt)

    this.userService.getUser(this.jwt)
      .subscribe(res => {
        if (res.status == 200)
          console.log(res.body);
      })

  }

  onClick() {
    this.savedText = this.text
  }
  update(event: any) {
    this.text = event.target.value
  }
}