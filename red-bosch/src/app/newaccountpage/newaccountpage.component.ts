import { Component, Input, Output, EventEmitter } from '@angular/core';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { RegisterDTO } from 'src/DTO/RegisterDTO';

@Component({
  selector: 'app-newaccountpage',
  templateUrl: './newaccountpage.component.html',
  styleUrls: ['./newaccountpage.component.css']
})

export class NewaccountpageComponent {
  // Inputs podem ser acessados de fora do componente como propriedades HTML
  // Outputs podem ser acessados de fora do componente como eventos no estilo onclick
  @Output() valueChanged = new EventEmitter<string>();
  @Input() breakLineOnInput = true;
  @Input() canSeePassword = true;
  @Input() seePassword = false
  @Output() seePasswordChanged = new EventEmitter<boolean>();
  
  protected checked = false ;
  protected inputType = "password";
  protected inputStyle = "color: black;"
  protected inputText = "";
  protected initialState = true;
  
  ngOnInit(): void {
  
  }

  protected showPassword() {
    if(this.checked)
      this.inputType = "text";
    else
      this.inputType = "password";
    
  }
}