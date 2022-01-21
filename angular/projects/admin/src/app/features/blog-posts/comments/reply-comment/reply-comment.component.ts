import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { CommentsService } from 'projects/shared/src/lib/services/api/comments/comments.service';
import { BlogComment } from 'projects/shared/src/lib/data/blog/comment';
import { CreateBlogComent } from 'projects/shared/src/lib/data/blog/create-blog-comment';

@Component({
  selector: 'app-reply-comment',
  templateUrl: './reply-comment.component.html',
  styleUrls: ['./reply-comment.component.scss']
})
export class ReplyCommentComponent implements OnInit, AfterViewInit {

  @Input() modal: NgbModalRef | null = null;
  @Input() comment: BlogComment = { comments: [], content:'', id: 0, isAuthor: true, name: '' };
  model: CreateBlogComent = { blogPostId: null, content: '', email: null, name: 'Author', parentCommentId: null}
  
  constructor(private readonly commentsService: CommentsService) { }

  ngOnInit(): void {
    if(this.comment.id == null || this.comment.id == 0) this.modal?.close();

    this.model.parentCommentId = this.comment?.id;

    
  }

  ngAfterViewInit(): void {
  }

  reply() {
    this.commentsService.postComment(this.model).subscribe((result) => {
      if(result.succeeded) this.modal?.close();
    })
  }

}
