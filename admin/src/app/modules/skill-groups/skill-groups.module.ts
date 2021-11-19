import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateSkillGroupComponent } from './create-skill-group/create-skill-group.component';
import { DeleteSkillGroupComponent } from './delete-skill-group/delete-skill-group.component';
import { ListSkillGroupComponent } from './list-skill-group/list-skill-group.component';
import { CreateSkillComponent } from './skills/create-skill/create-skill.component';
import { EditSkillComponent } from './skills/edit-skill/edit-skill.component';
import { DeleteSkillComponent } from './skills/delete-skill/delete-skill.component';
import { ListSkillComponent } from './skills/list-skill/list-skill.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SkillGroupRoutes } from './skill-groups.route';
import { EditSkillGroupComponent } from './edit-skill-group/edit-skill-group.component';



@NgModule({
  declarations: [CreateSkillGroupComponent, DeleteSkillGroupComponent, ListSkillGroupComponent, CreateSkillComponent, EditSkillComponent, DeleteSkillComponent, ListSkillComponent, EditSkillGroupComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(SkillGroupRoutes),
    FormsModule,
    ReactiveFormsModule,
],
})
export class SkillGroupsModule { }
