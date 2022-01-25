import { Routes } from "@angular/router";
import { ListEmailQueueComponent } from "./list-email-queue/list-email-queue.component";

export const EmailQueueRoutes: Routes = [{
    path: 'system/email-queue',
    component: ListEmailQueueComponent
}]