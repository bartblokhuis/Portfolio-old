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
import { DeleteUrlComponent } from './urls/delete-url/delete-url.component';
import { EditUrlComponent } from './urls/edit-url/edit-url.component';
import { AddUrlComponent } from './urls/add-url/add-url.component';
import { ListUrlComponent } from './urls/list-url/list-url.component';
import { ListProjectPicturesComponent } from './pictures/list-project-pictures/list-project-pictures.component';
import { EditProjectPictureComponent } from './pictures/edit-project-picture/edit-project-picture.component';
import { AddProjectPictureComponent } from './pictures/add-project-picture/add-project-picture.component';
import { DeleteProjectPictureComponent } from './pictures/delete-project-picture/delete-project-picture.component';

@NgModule({
  declarations: [
    ListProjectsComponent,
    AddProjectComponent,
    EditProjectComponent,
    DeleteProjectComponent,
    DeleteUrlComponent,
    EditUrlComponent,
    AddUrlComponent,
    ListUrlComponent,
    ListProjectPicturesComponent,
    EditProjectPictureComponent,
    AddProjectPictureComponent,
    DeleteProjectPictureComponent
  ],
  imports: [
    CommonModule,
    QuillModule,
    FormsModule,
    RouterModule.forChild(ProjectRoutes),
  ]
})
export class ProjectsModule { }
