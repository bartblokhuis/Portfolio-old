import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingComponent } from './landing/landing.component';
import { RouterModule } from '@angular/router';
import { LandingRoutes } from './landing.routes';
import { HomeComponent } from './home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/components/shared/shared.module';
import { AboutComponent } from './about/about.component';
import { SkillsComponent } from './skills/skills.component';
import { ContactComponent } from './contact/contact.component';
import { GlobalDirectivesModule } from 'src/app/modules/directives/global-directives/global-directives.module';
import { ListProjectComponent } from './projects/list-project/list-project.component';
import { ProjectComponent } from './projects/project/project.component';
import { NgxUsefulSwiperModule } from 'ngx-useful-swiper';



@NgModule({
  declarations: [
    LandingComponent,
    HomeComponent,
    AboutComponent,
    SkillsComponent,
    ContactComponent,
    ListProjectComponent,
    ProjectComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    NgxUsefulSwiperModule,
    GlobalDirectivesModule,
    RouterModule.forChild(LandingRoutes),
  ]
})
export class LandingModule { }
