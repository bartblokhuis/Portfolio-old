import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutMeComponent } from './about-me/about-me.component';
import { LandingComponent } from './landing/landing.component';
import { RouterModule } from '@angular/router';
import { LandingRoutes } from './landing.routes';
import { SkillsComponent } from './skills/skills.component';
import { ProjectsComponent } from './projects/projects.component';
import { SharedModule } from 'src/app/components/shared/shared.module';
import { GlobalDirectivesModule } from '../directives/global-directives/global-directives.module';



@NgModule({
  declarations: [
    AboutMeComponent,
    LandingComponent,
    SkillsComponent,
    ProjectsComponent,
  ],
  imports: [
    GlobalDirectivesModule,
    CommonModule,
    SharedModule,
    RouterModule.forChild(LandingRoutes),
  ],
  providers: [  ]
})
export class LandingModule { }
