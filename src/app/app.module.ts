import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CmsComponent } from './modules/cms/cms.component';
import { HomeComponent } from './modules/pages/home/home.component';
import { LoginComponent } from './modules/pages/login/login.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { FormPqrComponent } from './modules/pages/form-pqr/form-pqr.component';
import { DashboardComponent } from './modules/pages/dashboard/dashboard.component';
import { SigninComponent } from './modules/pages/signin/signin.component';
import { PageNotFoundComponent } from './modules/pages/page-not-found/page-not-found.component';
import { Images } from './constants/images';
import { DetailsPqrComponent } from './modules/pages/details-pqr/details-pqr.component';
import { DatePipe } from '@angular/common';
import { MessageComponent } from './modules/pages/message/message.component';
import { FormEditMessageComponent } from './modules/pages/message/form-edit-message/form-edit-message.component';
import { FormCreateMessageComponent } from './modules/pages/message/form-create-message/form-create-message.component';
import { UserAdminComponent } from './modules/pages/user-admin/user-admin.component';
import { FormCreateUserAdminComponent } from './modules/pages/user-admin/form-create-user-admin/form-create-user-admin.component';
import { FormEditUserAdminComponent } from './modules/pages/user-admin/form-edit-user-admin/form-edit-user-admin.component';
import { AngularEditorModule  } from '@kolkov/angular-editor';
import { NgxPaginationModule } from 'ngx-pagination';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AssignComponent } from './modules/pages/assign/assign.component';
import { EmployeeComponent } from './modules/pages/employee/employee.component';
import { FormCreateEmployeeComponent } from './modules/pages/employee/form-create-employee/form-create-employee.component';
import { FormEditEmployeeComponent } from './modules/pages/employee/form-edit-employee/form-edit-employee.component';
import { FormCreateAssignComponent } from './modules/pages/assign/form-create-assign/form-create-assign.component';
import { FormEditAssignComponent } from './modules/pages/assign/form-edit-assign/form-edit-assign.component';
import  {ModalComponent } from './components/modal/modal.component';
import { ConsultComponent } from './modules/pages/consult/consult.component';




@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    CmsComponent,
    NavbarComponent,
    SidebarComponent,
    FormPqrComponent,
    DashboardComponent,
    SigninComponent,
    PageNotFoundComponent,
    DetailsPqrComponent,
    MessageComponent,
    FormEditMessageComponent,
    FormCreateMessageComponent,
    UserAdminComponent,
    FormCreateUserAdminComponent,
    FormEditUserAdminComponent,
    AssignComponent,
    EmployeeComponent,
    FormCreateEmployeeComponent,
    FormEditEmployeeComponent,
    FormCreateAssignComponent,
    FormEditAssignComponent,
    ModalComponent,
    ConsultComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AngularEditorModule,
    NgxPaginationModule, 
    MatDialogModule,
    BrowserAnimationsModule,
    

  ],
  providers: [
    Images,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
