import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedPageComponent } from './feedpage/feedpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { NewaccountpageComponent } from './newaccountpage/newaccountpage.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { ForumPageComponent } from './forum-page/forum-page.component';
import { CreateForumPageComponent } from './create-forum-page/create-forum-page.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { ForumPageDetailsComponent } from './forum-page-details/forum-page-details.component';
import { ViewProfileComponent } from './view-profile/view-profile.component';

const routes: Routes = [
    { path: "", title: "Feed", component: FeedPageComponent },
    {
        path: "login",
        title: "Autentificação",
        component: LoginpageComponent,
    },
    { path: "newaccount", title: "Cadastro", component: NewaccountpageComponent},
    { path: "forumPage/:id", title: "Fórums", component: ForumPageDetailsComponent },
    { path: "forumPage", title: "Fórums", component: ForumPageComponent },
    { path: "createForumPage", title: "New Fórum", component: CreateForumPageComponent },
    { path: "editProfile", title: "Edit Profile", component: EditProfileComponent },
    { path: "profile/:name", title: "Profile", component: ViewProfileComponent },
    { path: "**", title: "Not Found", component: NotFoundPageComponent }
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }