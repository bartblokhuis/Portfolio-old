import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListBlogPostComponent } from './list-blog-post/list-blog-post.component';
import { BlogPostsRoutes } from './blog-posts.routes';
import { RouterModule } from '@angular/router';
import { AddBlogPostComponent } from './add-blog-post/add-blog-post.component';
import { FormsModule } from '@angular/forms';
import { EditBlogPostComponent } from './edit-blog-post/edit-blog-post.component';
import { DeleteBlogPostComponent } from './delete-blog-post/delete-blog-post.component';
import { DeleteCommentComponent } from './comments/delete-comment/delete-comment.component';
import { ReplyCommentComponent } from './comments/reply-comment/reply-comment.component';
import { PictureModule } from '../../components/picture/picture.module';
import { RichTextEditorModule } from '../../components/rich-text-editor/rich-text-editor.module';
import { SharedComponentsModule } from '../../components/shared/shared-components.module';
import { PipesModule } from '../../pipes/pipes.module';
import { DataTablesModule } from 'angular-datatables';



@NgModule({
  declarations: [
    ListBlogPostComponent,
    AddBlogPostComponent,
    EditBlogPostComponent,
    DeleteBlogPostComponent,
    DeleteCommentComponent,
    ReplyCommentComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharedComponentsModule,
    RichTextEditorModule,
    PictureModule,
    PipesModule,
    DataTablesModule,
    RouterModule.forChild(BlogPostsRoutes),
  ]
})
export class BlogPostsModule { }
