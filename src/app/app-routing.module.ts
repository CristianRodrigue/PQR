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
import { DetailsPqrComponent } from './modules/pages/details-pqr/details-pqr.component';
import { MessageComponent } from './modules/pages/message/message.component';
import { FormCreateMessageComponent } from './modules/pages/message/form-create-message/form-create-message.component';
import { FormEditMessageComponent } from './modules/pages/message/form-edit-message/form-edit-message.component';
import { UserAdminComponent } from './modules/pages/user-admin/user-admin.component';
import { FormCreateUserAdminComponent } from './modules/pages/user-admin/form-create-user-admin/form-create-user-admin.component';
import { FormEditUserAdminComponent } from './modules/pages/user-admin/form-edit-user-admin/form-edit-user-admin.component';
import { AssignComponent } from './modules/pages/assign/assign.component';
import { EmployeeComponent } from './modules/pages/employee/employee.component';
import { FormCreateAssignComponent } from './modules/pages/assign/form-create-assign/form-create-assign.component';
import { FormCreateEmployeeComponent } from './modules/pages/employee/form-create-employee/form-create-employee.component';
import { ConsultComponent } from './modules/pages/consult/consult.component';

const routes: Routes = [
  { path: '', component: CmsComponent,
  children: [
    { path: 'dashboard', component: DashboardComponent, canActivate: [ AuthGuard ] },  
    { path: 'pqrdetails/:id', component: DetailsPqrComponent, canActivate: [ AuthGuard ] }, 
    { path: 'mensajes', component: MessageComponent, canActivate: [ AuthGuard ] },
    { path: 'mensajes/create', component: FormCreateMessageComponent, canActivate: [ AuthGuard ] },
    { path: 'mensajes/edit/:id', component: FormEditMessageComponent, canActivate: [ AuthGuard ] },
    { path: 'usuarios', component: UserAdminComponent, canActivate: [ AuthGuard ] },
    { path: 'usuarios/create', component: FormCreateUserAdminComponent, canActivate: [ AuthGuard ] },
    { path: 'usuarios/edit/:id', component: FormEditUserAdminComponent, canActivate: [ AuthGuard ] },
    { path: 'asignacion', component: AssignComponent, canActivate: [ AuthGuard ] },
    //{ path: 'usuarios/create', component: FormCreateUserAdminComponent, canActivate: [ AuthGuard ] },
    { path: 'empleados', component: EmployeeComponent, canActivate: [ AuthGuard ] },
    //  { path: 'usuarios/create', component: FormCreateUserAdminComponent, canActivate: [ AuthGuard ] },
    { path: 'asignar/create', component: FormCreateAssignComponent, canActivate: [ AuthGuard ] },

    { path: 'empleados/create', component: FormCreateEmployeeComponent, canActivate: [ AuthGuard ] },
    

    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  ]},

  { path: 'login', component: LoginComponent },
  { path: 'registro', component: SigninComponent },
  { path: 'home', component: HomeComponent },
  { path: 'pqr', component: FormPqrComponent },
  { path: 'consultar', component: ConsultComponent },
  
  { path: '**', component: PageNotFoundComponent}
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
