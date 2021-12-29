import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BehaviorSubject, Observable } from 'rxjs';
import { ListBlog } from 'src/app/data/blog/list-blog';
import { Result } from 'src/app/data/common/Result';
import { ApiService } from 'src/app/services/api/api.service';
import { DeleteBlogComponent } from '../delete-blog/delete-blog.component';

@Component({
  selector: 'app-list-blog',
  templateUrl: './list-blog.component.html',
  styleUrls: ['./list-blog.component.scss']
})
export class ListBlogComponent implements OnInit {

  blogPosts: ListBlog[] = [];

  constructor(private apiService: ApiService, private router: Router,private modalService: NgbModal) { }

  ngOnInit(): void {
    this.loadBlog();
  }

  loadBlog() : void {
    this.apiService.get<ListBlog[]>('Blog?includeUnPublished=true').subscribe((result: Result<ListBlog[]>) => {
      this.blogPosts = result.data;
    })
  }

  editBlogPost(id: number) {
    this.router.navigate(['/blog/edit', id]);
  }

  deleteBlogPost(blog: ListBlog) {
    const modalRef = this.modalService.open(DeleteBlogComponent, { size: 'lg' });

    const editBlogPost: ListBlog = { createdAtUTC: blog.createdAtUTC, description: blog.description, displayNumber: blog.displayNumber, id: blog.id, isPublished: blog.isPublished, title: blog.title, updatedAtUtc: blog.updatedAtUtc };
    modalRef.componentInstance.blogPost = editBlogPost
    modalRef.componentInstance.modal = modalRef;
    
    modalRef.result.then(() => {
      this.loadBlog();
    });
  }

}
