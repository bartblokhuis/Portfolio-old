import { Component, OnInit } from '@angular/core';
import { environment } from 'projects/admin/src/environments/environment';
import { ListBlog } from '../../../data/blog/list-blog';
import { Result } from '../../../data/common/result';
import { ApiService } from '../../../services/common/api.service';

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
