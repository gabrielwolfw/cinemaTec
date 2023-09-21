import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { ClienteLoginComponent } from './cliente-login/cliente-login.component';
import { MenuAdminComponent } from './menu-admin/menu-admin.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AdminLoginComponent,
    ClienteLoginComponent,
    MenuAdminComponent,


  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
