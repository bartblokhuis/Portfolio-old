import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashBoardRoutes } from './dashboard.routes';
import { RouterModule } from '@angular/router';
import { DashboardMessagesComponent } from './dashboard-messages/dashboard-messages.component';
import { SharedComponentsModule } from '../../components/shared/shared-components.module';
import { PipesModule } from '../../pipes/pipes.module';
import { DashboardBlogSubscribersComponent } from './dashboard-blog-subscribers/dashboard-blog-subscribers.component';
import { NgChartsModule } from 'ng2-charts';



@NgModule({
  declarations: [
    DashboardComponent,
    DashboardMessagesComponent,
    DashboardBlogSubscribersComponent
  ],
  imports: [
    CommonModule,
    SharedComponentsModule,
    PipesModule,
    NgChartsModule,
    RouterModule.forChild(DashBoardRoutes),
  ]
})
export class DashboardModule { }
