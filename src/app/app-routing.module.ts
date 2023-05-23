import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guard/auth.guard';
import { CmsComponent } from './modules/cms/cms.component';
import { DashboardComponent } from './modules/pages/dashboard/dashboard.component';
import { FormPqrComponent } from './modules/pages/form-pqr/form-pqr.component';
import { HomeComponent } from './modules/pages/home/home.component';
import { LoginComponent } from './modules/pages/login/login.component';
import { PageNotFoundComponent } from './modules/pages/page-not-found/page-not-found.component';
import { SigninComponent } from './modules/pages/signin/signin.component';
import { ManageMessagesComponent } from './modules/pages/manage-messages/manage-messages.component';

const routes: Routes = [
  { path: '', component: CmsComponent,
  children: [
    { path: 'dashboard', component: DashboardComponent, canActivate: [ AuthGuard ]  },  
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  ]},

  { path: 'login', component: LoginComponent },
  { path: 'registro', component: SigninComponent },
  { path: 'home', component: HomeComponent },
  { path: 'pqr', component: FormPqrComponent },
  { path: 'mensajes', component: ManageMessagesComponent },
  
  { path: '**', component: PageNotFoundComponent}
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
