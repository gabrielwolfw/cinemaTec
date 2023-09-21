import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { ClienteLoginComponent } from './cliente-login/cliente-login.component';
import { MenuAdminComponent } from './menu-admin/menu-admin.component';
<<<<<<< Updated upstream
import { RegisterMovieComponent } from './register-movie/register-movie.component';
import { RegisterSalaComponent } from './register-sala/register-sala.component';


=======
import { SucursalComponent } from './sucursal/sucursal.component';
import { RegisterSalaComponent } from './register-sala/register-sala.component';
import { ProyeccionesComponent } from './proyecciones/proyecciones.component';
>>>>>>> Stashed changes

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AdminLoginComponent,
    ClienteLoginComponent,
    MenuAdminComponent,
<<<<<<< Updated upstream
    RegisterMovieComponent,
    RegisterSalaComponent,
=======
    SucursalComponent,
    RegisterSalaComponent,
    ProyeccionesComponent,
>>>>>>> Stashed changes

  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
