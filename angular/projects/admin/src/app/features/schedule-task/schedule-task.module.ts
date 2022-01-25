import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListScheduleTaskComponent } from './list-schedule-task/list-schedule-task.component';
import { AddScheduleTaskComponent } from './add-schedule-task/add-schedule-task.component';
import { EditScheduleTaskComponent } from './edit-schedule-task/edit-schedule-task.component';
import { DeleteScheduleTaskComponent } from './delete-schedule-task/delete-schedule-task.component';
import { RouterModule } from '@angular/router';
import { ScheduleTaskRoutes } from './schedule-task.routes';
import { PipesModule } from '../../pipes/pipes.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ListScheduleTaskComponent,
    AddScheduleTaskComponent,
    EditScheduleTaskComponent,
    DeleteScheduleTaskComponent
  ],
  imports: [
    CommonModule,
    PipesModule,
    FormsModule,
    RouterModule.forChild(ScheduleTaskRoutes)
  ]
})
export class ScheduleTaskModule { }
