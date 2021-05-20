import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main/main.component';
import { RegistryComponent } from './registry/registry.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: '', component:  MainComponent},
  { path: 'Login', component:  LoginComponent},
  { path: 'Registry', component:  RegistryComponent},
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes, {scrollPositionRestoration: 'enabled'}),
    CommonModule
  ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
