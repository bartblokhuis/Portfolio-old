import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ListBlog } from 'projects/shared/src/lib/data/blog/list-blog';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { BlogPostsService } from 'projects/shared/src/lib/services/api/blog-posts/blog-posts.service';
import { DeleteBlogPostComponent } from '../delete-blog-post/delete-blog-post.component';

@Component({
  selector: 'app-list-blog',
  templateUrl: './list-blog-post.component.html',
  styleUrls: ['./list-blog-post.component.scss']
})
export class ListBlogPostComponent implements OnInit {

  blogPosts: ListBlog[] = [];

  constructor(private blogPostsService: BlogPostsService, private router: Router,private modalService: NgbModal) { }

  ngOnInit(): void {
    this.loadBlog();
  }

  loadBlog() : void {
    this.blogPostsService.getAll().subscribe((result: Result<ListBlog[]>) => {
      this.blogPosts = result.data;
    })
  }

  editBlogPost(id: number) {
    this.router.navigate(['/blog/edit', id]);
  }

  deleteBlogPost(blog: ListBlog) {
    const modalRef = this.modalService.open(DeleteBlogPostComponent, { size: 'lg' });

    const editBlogPost: ListBlog = { createdAtUTC: blog.createdAtUTC, description: blog.description, displayNumber: blog.displayNumber, id: blog.id, isPublished: blog.isPublished, title: blog.title, updatedAtUtc: blog.updatedAtUtc, thumbnail: blog.thumbnail };
    modalRef.componentInstance.blogPost = editBlogPost
    modalRef.componentInstance.modal = modalRef;
    
    modalRef.result.then(() => {
      this.loadBlog();
    });
  }
}
