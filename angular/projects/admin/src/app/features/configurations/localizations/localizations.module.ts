import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocalizationRoutes } from './locaLIZATION.routes';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(LocalizationRoutes),
  ]
})
export class LocalizationsModule { }
