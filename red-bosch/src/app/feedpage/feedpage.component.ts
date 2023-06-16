import { Component } from '@angular/core';

@Component({
  selector: 'app-forum',
  templateUrl: './feedpage.component.html',
  styleUrls: ['./feedpage.component.css']
})

export class FeedPageComponent{
  text = "Altere aqui..."
  savedText = ""
  onClick() {
    this.savedText = this.text
  }
  update(event: any) {
    this.text = event.target.value
  }
}