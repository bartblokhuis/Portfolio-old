import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ListBlog } from 'src/app/data/blog/list-blog';
import { Result } from 'src/app/data/common/Result';
import { ApiService } from 'src/app/services/api/api.service';

@Component({
  selector: 'app-list-blog',
  templateUrl: './list-blog.component.html',
  styleUrls: ['./list-blog.component.scss']
})
export class ListBlogComponent implements OnInit {

  blogPosts: ListBlog[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadBlog();
  }

  loadBlog() : void {
    this.apiService.get<ListBlog[]>('Blog?includeUnPublished=true').subscribe((result: Result<ListBlog[]>) => {
      this.blogPosts = result.data;
    })
  }

}
