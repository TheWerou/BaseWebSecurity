import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main/main.component';
import { RegistryComponent } from './registry/registry.component';
import { LoginComponent } from './login/login.component';
import { SiteComponent } from './site/site.component';
import { AuthguardGuard } from './Auth/authguard.guard';
import { PassResetComponent } from './pass-reset/pass-reset.component';
import { AcceptaccountComponent } from './acceptaccount/acceptaccount.component';
import { PassResetReqComponent } from './pass-reset-req/pass-reset-req.component';

const routes: Routes = [
  { path: '', component:  MainComponent},
  { path: 'Login', component:  LoginComponent},
  { path: 'Registry', component:  RegistryComponent},
  { path: 'Site', component:  SiteComponent, canActivate: [AuthguardGuard]},
  { path: 'ResetPassword', component:  PassResetComponent},
  { path: 'AcceptAccount', component:  AcceptaccountComponent},
  { path: 'ForgotPassword', component:  PassResetReqComponent},
  { path: '**',  redirectTo: '' },
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
