import { Component, Input, OnInit } from '@angular/core';
import { BlogComment } from 'projects/admin/src/app/data/blog/comment';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss']
})
export class CommentComponent implements OnInit {

  @Input() comment: BlogComment | null = null;

  showReplyForm: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  onReplied($event: BlogComment) {
    this.comment?.comments.unshift($event);
    this.showReplyForm = false;
  }

}
