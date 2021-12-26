import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListBlogComponent } from './list-blog/list-blog.component';
import { SharedComponentsModule } from 'src/app/components/shared/shared-components.module';
import { BlogRoutes } from './blog.routes';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    ListBlogComponent
  ],
  imports: [
    CommonModule,
    SharedComponentsModule,
    RouterModule.forChild(BlogRoutes),
  ]
})
export class BlogModule { }
