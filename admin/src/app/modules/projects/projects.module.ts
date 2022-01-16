import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListProjectsComponent } from './list-projects/list-projects.component';
import { ProjectRoutes } from './projects.routes';
import { RouterModule } from '@angular/router';
import { AddProjectComponent } from './add-project/add-project.component';
import { QuillModule } from 'ngx-quill';
import { FormsModule } from '@angular/forms';
import { EditProjectComponent } from './edit-project/edit-project.component';
import { DeleteProjectComponent } from './delete-project/delete-project.component';

@NgModule({
  declarations: [
    ListProjectsComponent,
    AddProjectComponent,
    EditProjectComponent,
    DeleteProjectComponent
  ],
  imports: [
    CommonModule,
    QuillModule,
    FormsModule,
    RouterModule.forChild(ProjectRoutes),
  ]
})
export class ProjectsModule { }
