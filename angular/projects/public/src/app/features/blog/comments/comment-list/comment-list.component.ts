import { Component, Input, OnInit } from '@angular/core';
import { BlogComment } from 'projects/admin/src/app/data/blog/comment';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.scss']
})
export class CommentListComponent implements OnInit {
  
  @Input() comments: BlogComment[] = []

  constructor() { }

  ngOnInit(): void {
  }

}
