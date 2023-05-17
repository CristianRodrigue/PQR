import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CmsComponent } from './modules/cms/cms.component';
import { DashboardComponent } from './modules/pages/dashboard/dashboard.component';
import { FormPqrComponent } from './modules/pages/form-pqr/form-pqr.component';
import { LoginComponent } from './modules/pages/login/login.component';
import { PageNotFoundComponent } from './modules/pages/page-not-found/page-not-found.component';
import { SigninComponent } from './modules/pages/signin/signin.component';

const routes: Routes = [
  { path: '', component: CmsComponent,
  children: [
    { path: 'dashboard', component: DashboardComponent },
    { path: 'pqr', component: FormPqrComponent },
    
    
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  ]},

  { path: 'login', component: LoginComponent },
  { path: 'registro', component: SigninComponent },
  
  { path: '**', component: PageNotFoundComponent}
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
