import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ListBlog } from '../../../data/blog/list-blog';
import { Result } from '../../../data/common/Result';
import { BlogPostsService } from '../../../services/api/blog-posts/blog-posts.service';
import { NotificationService } from '../../../services/notification/notification.service';

@Component({
  selector: 'app-delete-blog',
  templateUrl: './delete-blog-post.component.html',
  styleUrls: ['./delete-blog-post.component.scss']
})
export class DeleteBlogPostComponent implements OnInit {
  
  @Input() modal: NgbModalRef | null = null;
  @Input() blogPost: ListBlog | null = null;

  constructor(private blogPostsService: BlogPostsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  remove(): void {

    if(!this.blogPost) return;

    this.blogPostsService.deleteBlogPost(this.blogPost.id).subscribe((result: Result<any>) => {
      if(!result.succeeded) this.notificationService.success(`Failed to remove the blog post`)
      else this.notificationService.success(`Removed blog post: ${this.blogPost?.title}`)
      this.modal?.close("removed");
    });
  }

}
