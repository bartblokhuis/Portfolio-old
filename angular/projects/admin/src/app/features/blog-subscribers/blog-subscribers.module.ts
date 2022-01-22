import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListBlogSubscriberComponent } from './list-blog-subscriber/list-blog-subscriber.component';
import { RouterModule } from '@angular/router';
import { BlogSubscriberRoutes } from './blog-subscribers.routes';
import { FormsModule } from '@angular/forms';
import { PipesModule } from '../../pipes/pipes.module';



@NgModule({
  declarations: [
    ListBlogSubscriberComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    PipesModule,
    RouterModule.forChild(BlogSubscriberRoutes),
  ]
})
export class BlogSubscribersModule { }
