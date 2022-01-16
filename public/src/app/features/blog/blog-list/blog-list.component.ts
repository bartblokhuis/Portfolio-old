import { Component, OnInit } from '@angular/core';
import { ListBlog } from 'src/app/data/blog/list-blog';
import { Result } from 'src/app/data/common/result';
import { ApiService } from 'src/app/services/common/api.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-blog-list',
  templateUrl: './blog-list.component.html',
  styleUrls: ['./blog-list.component.scss']
})
export class BlogListComponent implements OnInit {

  baseUrl = environment.baseApiUrl;
  blogPosts: ListBlog[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.apiService.get<ListBlog[]>('BlogPost?includeUnPublished=false').subscribe((result: Result<ListBlog[]>) => {
      this.blogPosts = result.data;
    })
  }

}
