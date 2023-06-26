import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedPageComponent } from './feedpage/feedpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { NewaccountpageComponent } from './newaccountpage/newaccountpage.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { ForumPageComponent } from './forum-page/forum-page.component';
import { CreateForumPageComponent } from './create-forum-page/create-forum-page.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';

const routes: Routes = [
    { path: "", title: "Feed", component: FeedPageComponent },
    {
        path: "login",
        title: "Autentificação",
        component: LoginpageComponent,
        children: [
            { path: "newaccount", title: "Autentificação", component: NewaccountpageComponent }
        ]
    },
    { path: "forumPage/:name", title: "Fórums", component: ForumPageComponent },
    { path: "forumPage", title: "Fórums", component: ForumPageComponent },
    { path: "createForumPage", title: "New Fórum", component: CreateForumPageComponent },
    { path: "editProfile", title: "Edit Profile", component: EditProfileComponent },
    { path: "**", title: "Not Found", component: NotFoundPageComponent }
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }