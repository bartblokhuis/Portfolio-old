import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlogListComponent } from './blog-list/blog-list.component';
import { BlogRoutes } from './bog.routes';
import { RouterModule } from '@angular/router';
import { BlogPostComponent } from './blog-post/blog-post.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommentComponent } from './comments/comment/comment.component';
import { CommentListComponent } from './comments/comment-list/comment-list.component';
import { CommentNewComponent } from './comments/comment-new/comment-new.component';
import { CommentReplyComponent } from './comments/comment-reply/comment-reply.component';
import { SharedModule } from '../../components/shared/shared.module';
import { BlogSubscribeComponent } from './subscribers/blog-subscribe/blog-subscribe.component';
import { BlogUnsubscribeComponent } from './subscribers/blog-unsubscribe/blog-unsubscribe.component';



@NgModule({
  declarations: [
    BlogListComponent,
    BlogPostComponent,
    CommentComponent,
    CommentListComponent,
    CommentNewComponent,
    CommentReplyComponent,
    BlogSubscribeComponent,
    BlogUnsubscribeComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(BlogRoutes),
  ]
})
export class BlogModule { }
