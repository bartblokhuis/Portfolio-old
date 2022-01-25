import { Routes } from "@angular/router";
import { ListScheduleTaskComponent } from "./list-schedule-task/list-schedule-task.component";

export const ScheduleTaskRoutes: Routes = [{
    path: 'system/schedule-tasks',
    component: ListScheduleTaskComponent
}]