import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { ClienteLoginComponent } from './cliente-login/cliente-login.component';
import { MenuAdminComponent } from './menu-admin/menu-admin.component';
import { RegisterMovieComponent } from './register-movie/register-movie.component';



const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'adminLogin', component: AdminLoginComponent},
  { path: 'clienteLogin', component: ClienteLoginComponent},
  {path: 'menuAdmin', component: MenuAdminComponent},
  {path: 'registerMovie', component: RegisterMovieComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
