import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListSkillGroupsComponent } from './list-skill-groups/list-skill-groups.component';
import { SkillGroupRoutes } from './skill-groups.routes';
import { RouterModule } from '@angular/router';
import { CreateSkillGroupComponent } from './create-skill-group/create-skill-group.component';
import { FormsModule } from '@angular/forms';
import { DeleteSkillGroupComponent } from './delete-skill-group/delete-skill-group.component';
import { ListSkillsComponent } from './skills/list-skills/list-skills.component';
import { CreateSkillComponent } from './skills/create-skill/create-skill.component';
import { EditSkillComponent } from './skills/edit-skill/edit-skill.component';
import { DeleteSkillComponent } from './skills/delete-skill/delete-skill.component';



@NgModule({
  declarations: [
    ListSkillGroupsComponent,
    CreateSkillGroupComponent,
    DeleteSkillGroupComponent,
    ListSkillsComponent,
    CreateSkillComponent,
    EditSkillComponent,
    DeleteSkillComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(SkillGroupRoutes),
  ]
})
export class SkillGroupsModule { }
