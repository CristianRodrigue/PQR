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
import { ManageMessagesComponent } from './modules/pages/manage-messages/manage-messages.component';
import { DetailsPqrComponent } from './modules/pages/details-pqr/details-pqr.component';
import { DatePipe } from '@angular/common';


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
    ManageMessagesComponent,
    DetailsPqrComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    Images,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
