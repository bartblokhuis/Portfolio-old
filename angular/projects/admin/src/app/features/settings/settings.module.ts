import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SettingRoutes } from './settings.routes';
import { GeneralSettingsComponent } from './general-settings/general-settings.component';
import { SeoSettingsComponent } from './seo-settings/seo-settings.component';
import { EmailSettingsComponent } from './email-settings/email-settings.component';
import { FormsModule } from '@angular/forms';
import { BlogSettingsComponent } from './blog-settings/blog-settings.component';
import { PublicSiteSettingsComponent } from './public-site-settings/public-site-settings.component';


@NgModule({
  declarations: [
    GeneralSettingsComponent,
    SeoSettingsComponent,
    EmailSettingsComponent,
    BlogSettingsComponent,
    PublicSiteSettingsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(SettingRoutes),
  ]
})
export class SettingsModule { }
