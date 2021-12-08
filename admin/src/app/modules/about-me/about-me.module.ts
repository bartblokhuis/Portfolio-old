import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutMeComponent } from './about-me/about-me.component';
import { RouterModule } from '@angular/router';
import { AboutMeRoutes } from './about-me.routes';
import { QuillModule } from 'ngx-quill';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    AboutMeComponent
  ],
  imports: [
    CommonModule,
    QuillModule,
    FormsModule,
    RouterModule.forChild(AboutMeRoutes),
  ]
})
export class AboutMeModule { }
