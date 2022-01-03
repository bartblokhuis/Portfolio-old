import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlogListComponent } from './blog-list/blog-list.component';
import { BlogRoutes } from './bog.routes';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    BlogListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(BlogRoutes),
  ]
})
export class BlogModule { }
