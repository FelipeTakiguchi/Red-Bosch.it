import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.css']
})

export class PasswordComponent implements OnInit {
  // Inputs podem ser acessados de fora do componente como propriedades HTML
  // Outputs podem ser acessados de fora do componente como eventos no estilo onclick
  @Output() valueChanged = new EventEmitter<string>();
  @Input() breakLineOnInput = true;
  @Input() canSeePassword = true;
  @Input() seePassword = false
  @Output() seePasswordChanged = new EventEmitter<boolean>();

  protected checked = false;
  protected inputType = "password";
  protected inputStyle = "color: black;"
  protected inputText = "";
  protected initialState = true;

  ngOnInit(): void {

  }

  protected showPassword() {
    if (this.checked)
      this.inputType = "text";
    else
      this.inputType = "password";

  }

  protected passwordChanged() {
    this.valueChanged.emit(this.inputText)
  }
}