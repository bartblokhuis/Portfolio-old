import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogComment } from 'projects/shared/src/lib/data/blog/comment';
import { CreateBlogComent } from 'projects/shared/src/lib/data/blog/create-blog-comment';
import { Result } from 'projects/shared/src/lib/data/common/Result';
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
