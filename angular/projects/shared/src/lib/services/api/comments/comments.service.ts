import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogComment } from 'projects/shared/src/lib/data/blog/comment';
import { CreateBlogComent } from 'projects/shared/src/lib/data/blog/create-blog-comment';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  constructor(private apiService: ApiService) { }

  getCommentsByBlogPostId(blogPostId: number) {
    return this.apiService.get<BlogComment[]>(`BlogPost/Comments/GetByBlogPostId?blogPostId=${blogPostId}`);
  }

  deleteComment(id: number): Observable<Result> {
    return this.apiService.delete(`BlogPost/Comment?id=${id}`);
  }

  postComment(comment: CreateBlogComent): Observable<Result<BlogComment>> {
    return this.apiService.post<BlogComment>("BlogPost/Comment", comment);
  }
}
