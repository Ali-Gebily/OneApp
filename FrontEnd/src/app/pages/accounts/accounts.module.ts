import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgaModule } from '../../theme/nga.module';
import { Routes, RouterModule } from '@angular/router';

import { AccountsService } from './services/accounts.service';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { ForgotPasswordComponent } from './components/forgotPassword/forgotPassword.component';
import { ChangePasswordComponent } from './components/changePassword/changePassword.component';




const routes: Routes = [
  {
    path: '',
    redirectTo: "login"
  },
  {
    path: 'register',
    component: RegisterComponent
  }
  ,
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'forgotPassword',
    component: ForgotPasswordComponent
  },
  {
    path: 'changePassword',
    component: ChangePasswordComponent
  }
];



@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgaModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    LoginComponent, RegisterComponent, ForgotPasswordComponent, ChangePasswordComponent
  ],
  providers: [
    AccountsService
  ]
})
export default class AccountsModule { }
