import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguagesListComponent } from './languages-list/languages-list.component';
import { LanguageRoutes } from './language.routes';
import { RouterModule } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { LanguagesAddComponent } from './languages-add/languages-add.component';
import { LanguagesUpdateComponent } from './languages-update/languages-update.component';
import { LanguagesDeleteComponent } from './languages-delete/languages-delete.component';



@NgModule({
  declarations: [
    LanguagesListComponent,
    LanguagesAddComponent,
    LanguagesUpdateComponent,
    LanguagesDeleteComponent
  ],
  imports: [
    CommonModule,
    DataTablesModule,
    RouterModule.forChild(LanguageRoutes),
  ]
})
export class LanguagesModule { }
