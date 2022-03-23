import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfigurationRoutes } from './configuration.routes';
import { SettingsModule } from './settings/settings.module';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SettingsModule,
    RouterModule.forChild(ConfigurationRoutes),
  ]
})
export class ConfigurationsModule { }
