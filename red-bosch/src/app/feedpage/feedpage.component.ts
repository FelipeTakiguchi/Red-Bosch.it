import { Component, Input, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { Jwt } from 'src/DTO/Jwt';

@Component({
  selector: 'app-forum',
  templateUrl: './feedpage.component.html',
  styleUrls: ['./feedpage.component.css']
})

export class FeedPageComponent implements OnInit {
  @Input() Nome: string | undefined;
  @Input() Email: string | undefined;
  @Input() Idade: number | undefined;

  text = "Altere aqui..."
  savedText = ""

  constructor(private userService: UserService) { }

  jwt: Jwt = {
    jwt: '',
  }

  ngOnInit(): void {
    if (sessionStorage.getItem("jwtSession") != null)
      this.jwt.jwt = sessionStorage.getItem("jwtSession")!

    console.log(this.jwt.jwt)
    console.log(typeof (this.jwt))

    this.userService.getUser(this.jwt)
      .subscribe(res => {
        if (res.status == 200) {
          console.log(res.body);
          this.Nome = res.body?.nome;
          this.Email = res.body?.email;
          const Nascimento = new Date(new String(res.body?.dataNascimento).toString());
          let timeDiff = Math.abs(Date.now() - Nascimento.getTime());
          this.Idade = Math.floor((timeDiff / (1000 * 3600 * 24))/365.25);
          console.log(this.Idade);
        }
      })

  }

  onClick() {
    this.savedText = this.text
  }
  update(event: any) {
    this.text = event.target.value
  }
}