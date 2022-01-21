import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserRoutes } from './user.routes';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { UserEditComponent } from './user-edit/user-edit.component';



@NgModule({
  declarations: [
    LoginComponent,
    UserEditComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(UserRoutes)
  ]
})
export class UserModule { }
