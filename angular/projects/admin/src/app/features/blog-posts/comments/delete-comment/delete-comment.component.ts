import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { BlogComment } from 'projects/admin/src/app/data/blog/comment';
import { CommentsService } from 'projects/admin/src/app/services/api/comments/comments.service';
import { NotificationService } from 'projects/admin/src/app/services/notification/notification.service';

@Component({
  selector: 'app-delete-comment',
  templateUrl: './delete-comment.component.html',
  styleUrls: ['./delete-comment.component.scss']
})
export class DeleteCommentComponent implements OnInit {
  
  @Input() modal: NgbModalRef | null = null;
  @Input() comment: BlogComment = { comments: [], content:'', id: 0, isAuthor: true, name: '' };
  
  constructor(private readonly commentsService: CommentsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  remove() {
    if(!this.comment) return;
    this.commentsService.deleteComment(this.comment.id).subscribe((result) => {
      if(result.succeeded){
        this.notificationService.success('Removed the comment')
        this.modal?.close();
      }
    });
  }

}
