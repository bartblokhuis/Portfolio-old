import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListBlogPostComponent } from './list-blog-post/list-blog-post.component';
import { SharedComponentsModule } from 'src/app/components/shared/shared-components.module';
import { BlogPostsRoutes } from './blog-posts.routes';
import { RouterModule } from '@angular/router';
import { AddBlogPostComponent } from './add-blog-post/add-blog-post.component';
import { QuillModule } from 'ngx-quill';
import { FormsModule } from '@angular/forms';
import { EditBlogPostComponent } from './edit-blog-post/edit-blog-post.component';
import { DeleteBlogPostComponent } from './delete-blog-post/delete-blog-post.component';



@NgModule({
  declarations: [
    ListBlogPostComponent,
    AddBlogPostComponent,
    EditBlogPostComponent,
    DeleteBlogPostComponent
  ],
  imports: [
    CommonModule,
    QuillModule,
    FormsModule,
    SharedComponentsModule,
    RouterModule.forChild(BlogPostsRoutes),
  ]
})
export class BlogPostsModule { }