import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogComment } from 'src/app/data/blog/comment';
import { CreateBlogComent } from 'src/app/data/blog/create-blog-comment';
import { Result } from 'src/app/data/common/result';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class BlogPostsService {

  constructor(private apiService: ApiService) { }

  postComment(comment: CreateBlogComent): Observable<Result<BlogComment>> {
    return this.apiService.post<BlogComment>("BlogPost/Comment", comment);
  }
}
