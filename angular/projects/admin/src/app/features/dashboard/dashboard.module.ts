import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashBoardRoutes } from './dashboard.routes';
import { RouterModule } from '@angular/router';
import { DashboardMessagesComponent } from './dashboard-messages/dashboard-messages.component';
import { SharedComponentsModule } from '../../components/shared/shared-components.module';
import { PipesModule } from '../../pipes/pipes.module';



@NgModule({
  declarations: [
    DashboardComponent,
    DashboardMessagesComponent
  ],
  imports: [
    CommonModule,
    SharedComponentsModule,
    PipesModule,
    RouterModule.forChild(DashBoardRoutes),
  ]
})
export class DashboardModule { }
