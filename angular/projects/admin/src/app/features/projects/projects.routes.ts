import { Routes } from "@angular/router";
import { AddProjectComponent } from "./add-project/add-project.component";
import { EditProjectComponent } from "./edit-project/edit-project.component";
import { ListProjectsComponent } from "./list-projects/list-projects.component";

export const ProjectRoutes: Routes = [{
    path: 'projects',
    component: ListProjectsComponent,
    pathMatch: 'full'
},
{
    path: 'projects/new',
    component: AddProjectComponent
},
{
    path: 'projects/edit/:id',
    component: EditProjectComponent
}]