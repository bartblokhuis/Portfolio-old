import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { UserRoutes } from './user.routes';
import { LoginComponent } from './login/login.component';
import { EditComponent } from './edit/edit.component';



@NgModule({
  declarations: [LoginComponent, EditComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(UserRoutes),
    FormsModule,
    ReactiveFormsModule,
]
})
export class UserModule { }
