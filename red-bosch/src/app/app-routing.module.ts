import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedPageComponent } from './feedpage/feedpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { NewaccountpageComponent } from './newaccountpage/newaccountpage.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { ForumPageComponent } from './forum-page/forum-page.component';


const routes: Routes = [
    { path: "", component: FeedPageComponent },
    {
        path: "login",
        title: "Autentificação",
        component: LoginpageComponent,
        children: [
            { path: "newaccount", component: NewaccountpageComponent }
        ]
    },
    { path: "forumPage", component: ForumPageComponent },
    { path: "**", component: NotFoundPageComponent }
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }