import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguagesListComponent } from './languages-list/languages-list.component';
import { LanguageRoutes } from './language.routes';
import { RouterModule } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';



@NgModule({
  declarations: [
    LanguagesListComponent
  ],
  imports: [
    CommonModule,
    DataTablesModule,
    RouterModule.forChild(LanguageRoutes),
  ]
})
export class LanguagesModule { }
