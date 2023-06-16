import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { NavComponent } from './nav/nav.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { ForumPageComponent } from './forum-page/forum-page.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { FeedPageComponent } from './feedpage/feedpage.component';
import { NewaccountpageComponent } from './newaccountpage/newaccountpage.component';
import { PasswordComponent } from './password/password.component';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { CreatePasswordComponent } from './create-password/create-password.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginpageComponent,
    ForumPageComponent,
    NotFoundPageComponent,
    FeedPageComponent,
    NewaccountpageComponent,
    PasswordComponent,
    CreatePasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }