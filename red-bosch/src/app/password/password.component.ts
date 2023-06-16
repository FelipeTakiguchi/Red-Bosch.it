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
  
  protected inputType = "text";
  protected inputStyle = "color: black;"
  protected inputText = "";
  protected initialState = true;
  
  ngOnInit(): void {
    this.updateInput()
  }
  
  protected checkBoxToogle(newValue: any) {
    this.updateInput()
    this.seePasswordChanged.emit(this.seePassword);
  }

  protected updateInput() {
    if (this.initialState) {
      this.inputText = "Escreva sua senha..."
      this.inputType = "text"
      this.inputStyle = "color: gray;"
      return
    }

    this.inputStyle = "color: black;"
    this.inputType = this.seePassword ? "text" : "password";
  }

  protected passwordChanged() {
    this.updateInput()
    this.valueChanged.emit(this.inputText)
  }

  protected passwordClick() {
    if (!this.initialState)
      return
    this.initialState = false;
    this.inputText = "";
    this.updateInput();
  }

  protected passwordFocusout() {
    if (this.inputText !== "")
      return
    this.initialState = true
    this.updateInput()
  }
}