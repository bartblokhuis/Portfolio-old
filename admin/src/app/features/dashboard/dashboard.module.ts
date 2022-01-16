import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashBoardRoutes } from './dashboard.routes';
import { RouterModule } from '@angular/router';
import { DashboardMessagesComponent } from './dashboard-messages/dashboard-messages.component';
import { SharedComponentsModule } from 'src/app/components/shared/shared-components.module';



@NgModule({
  declarations: [
    DashboardComponent,
    DashboardMessagesComponent
  ],
  imports: [
    CommonModule,
    SharedComponentsModule,
    RouterModule.forChild(DashBoardRoutes),
  ]
})
export class DashboardModule { }
