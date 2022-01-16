import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateBlog } from 'src/app/data/blog/create-blog';
import { EditBlog } from 'src/app/data/blog/edit-blog';
import { ListBlog } from 'src/app/data/blog/list-blog';
import { UpdateBlogPicture } from 'src/app/data/blog/update-blog-picture';
import { Result } from 'src/app/data/common/Result';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class BlogPostsService {

  constructor(private apiService: ApiService) { }

  getAll(): Observable<Result<ListBlog[]>> {
    return this.apiService.get<ListBlog[]>('BlogPost?includeUnPublished=true')
  }

  getById(id: number): Observable<Result<EditBlog>> {
    return this.apiService.get<EditBlog>(`BlogPost/GetById/?id=${id}&includeUnPublished=true`);
  }

  createBlogPost(blogPost: CreateBlog): Observable<Result<ListBlog>> {
    return this.apiService.post<ListBlog>("BlogPost", blogPost);
  }

  editBlogPost(blogPost: EditBlog): Observable<Result<EditBlog>> {
    return this.apiService.put<EditBlog>("BlogPost", blogPost);
  }

  updateThumbnail(blogPostPicture: UpdateBlogPicture): Observable<Result> {
    return this.apiService.put("BlogPost/UpdateThumbnailPicture", blogPostPicture);
  }

  updateBanner(blogPostPicture: UpdateBlogPicture): Observable<Result> {
    return this.apiService.put("BlogPost/UpdateBannerPicture", blogPostPicture);
  }

  deleteBlogPost(id: number): Observable<Result> {
    return this.apiService.delete(`BlogPost?id=${id}`);
  }
}