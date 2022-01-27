import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BlogComment } from 'projects/shared/src/lib/data/blog/comment';
import { CreateBlogComent } from 'projects/shared/src/lib/data/blog/create-blog-comment';
import { isValidEmail } from 'projects/shared/src/lib/helpers/common-helpers';
import { CommentsService } from 'projects/shared/src/lib/services/api/comments/comments.service';

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

  constructor(private readonly commentsService: CommentsService) { }

  ngOnInit(): void {
    if(this.comment != null){
      this.model.parentCommentId = this.comment?.id;
    }
  }

  createComment() {

    this.error = null;
    this.nameError = null;
    this.commentError = null;
    this.emailError = null;

    let hasError: boolean = false;
    if(!this.model.name || this.model.name.length < 1){
      this.nameError = "Please enter a name";
      hasError = true;
    }

    if(this.model.name.length > 64){
      this.nameError = "Please enter a name that has less than 65 charachters";
      hasError = true;
    }

   if(this.model.email && this.model.email.length > 0 && !isValidEmail(this.model.email)){
      this.emailError = "Please enter a valid email address";
      hasError = true;
    }

    if(!this.model.content || this.model.content.length < 1){
      this.commentError = "Please enter your comment";
      hasError = true;
    }
    if(this.model.content.length > 512){
      this.commentError = "Please don't enter a comment with more than 512 charachters";
      hasError = true;
    }

    if(hasError) return;

    this.commentsService.postComment(this.model).subscribe((result) => {
      if(result.succeeded) this.onReplied.emit(result.data);
    });
  }

  cancel() {
    this.onReplyCancel.emit();
  }
}
