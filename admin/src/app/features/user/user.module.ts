import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RouterModule } from '@angular/router';
import { UserRoutes } from './user.routes';
import { UserEditComponent } from './user-edit/user-edit.component';
import { FormsModule } from '@angular/forms';



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
