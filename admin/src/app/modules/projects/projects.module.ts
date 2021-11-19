import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

import { ListProjectComponent } from './list-project/list-project.component';
import { EditProjectComponent } from './edit-project/edit-project.component';
import { CreateProjectComponent } from './create-project/create-project.component';
import { ProjectRoutes } from './projects.route';
import { DeleteProjectComponent } from './delete-project/delete-project.component'
import { ComponentsModule } from '../../components/components.module';


@NgModule({
  declarations: [ListProjectComponent, EditProjectComponent, CreateProjectComponent, DeleteProjectComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(ProjectRoutes),
    FormsModule,
    ReactiveFormsModule,
    ComponentsModule,
    NgMultiSelectDropDownModule
  ]
})
export class ProjectsModule { }
