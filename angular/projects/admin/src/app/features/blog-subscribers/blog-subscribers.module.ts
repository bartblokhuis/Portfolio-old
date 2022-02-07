import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListBlogSubscriberComponent } from './list-blog-subscriber/list-blog-subscriber.component';
import { RouterModule } from '@angular/router';
import { BlogSubscriberRoutes } from './blog-subscribers.routes';
import { FormsModule } from '@angular/forms';
import { PipesModule } from '../../pipes/pipes.module';
import { DeleteBlogSubscriberComponent } from './delete-blog-subscriber/delete-blog-subscriber.component';
import { DataTablesModule } from 'angular-datatables';



@NgModule({
  declarations: [
    ListBlogSubscriberComponent,
    DeleteBlogSubscriberComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    PipesModule,
    DataTablesModule,
    RouterModule.forChild(BlogSubscriberRoutes),
  ]
})
export class BlogSubscribersModule { }
