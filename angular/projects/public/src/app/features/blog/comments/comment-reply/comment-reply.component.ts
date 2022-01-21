import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BlogComment } from 'projects/admin/src/app/data/blog/comment';
import { CreateBlogComent } from 'projects/admin/src/app/data/blog/create-blog-comment';
import { BlogPostsService } from 'projects/public/src/app/services/blogPosts/blog-posts.service';
import { isEmail } from '../comment-new/comment-new.component';

@Component({
  selector: 'app-comment-reply',
  templateUrl: './comment-reply.component.html',
  styleUrls: ['./comment-reply.component.scss']
})
export class CommentReplyComponent implements OnInit {

  @Input() comment: BlogComment | null = null;

  @Output() onReplied: EventEmitter<BlogComment> = new EventEmitter<BlogComment>();
  @Output() onReplyCancel: EventEmitter<any> = new EventEmitter();

  model : CreateBlogComent = { blogPostId: null, content: '', name: '', parentCommentId: null, email: '' };
  error: string | null = null;
  nameError: string | null = null;
  emailError: string | null = null;
  commentError: string | null = null;

  constructor(private readonly blogPostsService: BlogPostsService) { }

  ngOnInit(): void {
    if(this.comment != null){
      this.model.parentCommentId = this.comment?.id;
    }
  }

  createComment() {

    let hasError: boolean = false;
    if(!this.model.name || this.model.name.length < 1){
      this.nameError = "Please enter a name";
      hasError = true;
    }

    if(this.model.name.length > 16){
      this.nameError = "Please enter a name that has less than 16 charachters";
      hasError = true;
    }

   if(this.model.email && this.model.email.length > 0 && !isEmail(this.model.email)){
      this.emailError = "Please enter a valid email address";
      hasError = true;
    }

    if(!this.model.content || this.model.content.length < 1){
      this.commentError = "Please enter your comment";
      hasError = true;
    }

    if(hasError) return;

    this.blogPostsService.postComment(this.model).subscribe((result) => {
      if(result.succeeded) this.onReplied.emit(result.data);
    });
  }

  cancel() {
    this.onReplyCancel.emit();
  }
}
