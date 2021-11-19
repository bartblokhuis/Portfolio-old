import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SettingsRoutes } from './settings.routes';
import { EmailSettingsComponent } from './email-settings/email-settings.component';
import { SeoSettingsComponent } from './seo-settings/seo-settings.component';
import { GeneralSettingsComponent } from './general-settings/general-settings.component';

@NgModule({
  declarations: [EmailSettingsComponent, SeoSettingsComponent, GeneralSettingsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(SettingsRoutes),
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class SettingsModule { }
