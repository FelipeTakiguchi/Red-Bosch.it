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
import { CheckboxControlValueAccessor, FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { CreatePasswordComponent } from './create-password/create-password.component';
import {MatInputModule} from '@angular/material/input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatFormFieldModule, matFormFieldAnimations} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatCardModule} from '@angular/material/card';
import {MatDatepickerModule, matDatepickerAnimations} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatTabsModule} from '@angular/material/tabs';
import { ForumCardComponent } from './forum-card/forum-card.component';
import { ForumCardPageComponent } from './forum-card-page/forum-card-page.component';
import { ProfileCardComponent } from './profile-card/profile-card.component';
import { CreateForumPageComponent } from './create-forum-page/create-forum-page.component';
import {MatIconModule} from '@angular/material/icon';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { CustomInputComponent } from './custom-input/custom-input.component';
import { CustomProfileImgComponent } from './custom-profile-img/custom-profile-img.component';
import { UploaderComponent } from './uploader/uploader.component';
import { HttpClientModule } from '@angular/common/http';

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
    CreatePasswordComponent,
    ForumCardComponent,
    ForumCardPageComponent,
    ProfileCardComponent,
    CreateForumPageComponent,
    EditProfileComponent,
    CustomInputComponent,
    CustomProfileImgComponent,
    UploaderComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    MatInputModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatCheckboxModule,
    MatCardModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTabsModule,
    MatIconModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }