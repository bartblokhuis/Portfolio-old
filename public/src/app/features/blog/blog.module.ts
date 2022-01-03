import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlogListComponent } from './blog-list/blog-list.component';
import { BlogRoutes } from './bog.routes';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/components/shared/shared.module';



@NgModule({
  declarations: [
    BlogListComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(BlogRoutes),
  ]
})
export class BlogModule { }
