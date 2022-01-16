import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ListBlog } from 'src/app/data/blog/list-blog';
import { Result } from 'src/app/data/common/Result';
import { ApiService } from 'src/app/services/api/api.service';
import { NotificationService } from 'src/app/services/notification/notification.service';

@Component({
  selector: 'app-delete-blog',
  templateUrl: './delete-blog-post.component.html',
  styleUrls: ['./delete-blog-post.component.scss']
})
export class DeleteBlogPostComponent implements OnInit {
  
  @Input() modal: NgbModalRef | null = null;
  @Input() blogPost: ListBlog | null = null;

  constructor(private apiService: ApiService, private notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  remove(): void {

    if(!this.blogPost) return;

    this.apiService.delete(`BlogPost?id=${this.blogPost.id}`).subscribe((result: Result<any>) => {
      if(!result.succeeded) this.notificationService.success(`Failed to remove the blog post`)
      else this.notificationService.success(`Removed blog post: ${this.blogPost?.title}`)
      this.modal?.close("removed");
    });
  }

}
