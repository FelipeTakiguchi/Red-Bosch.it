import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserService } from '../services/user.service';
import { Jwt } from 'src/DTO/Jwt';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-profile',
  templateUrl: './view-profile.component.html',
  styleUrls: ['./view-profile.component.css']
})
export class ViewProfileComponent {
    @Output() valueChanged = new EventEmitter<string>();
  @Input() Nome: string | undefined;
  @Input() url: string | undefined;
  @Input() Descricao: string | undefined;
  @Input() Email: string | undefined;
  @Input() Idade: number | undefined;
  
  formData = new FormData();

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.Nome = location.pathname.split("/")[2].replace('%20', ' ');

    this.userService.getUserByName(this.Nome)
      .subscribe(usuario => {
        this.Nome = usuario.nome
        this.Descricao = usuario.descricao
        this.url = "http://localhost:5022/img/" + usuario.imageId
        const Nascimento = new Date(new String(usuario.dataNascimento).toString())
        let timeDiff = Math.abs(Date.now() - Nascimento.getTime())
        this.Idade = Math.floor((timeDiff / (1000 * 3600 * 24)) / 365.25)
        this.Email = usuario.email
      })
  }
}
