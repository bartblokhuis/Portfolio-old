import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogSubscriber } from '../../../data/blog-subscribers/blog-subscriber';
import { CreateBlogSubscriber } from '../../../data/blog-subscribers/create-blog-subscriber';
import { ListBlogSubscriber } from '../../../data/blog-subscribers/list-blog-subscriber';
import { Result } from '../../../data/common/Result';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class BlogSubscribersService {

  readonly baseUrl = "BlogSubscriber"

  constructor(private readonly apiService: ApiService) { }

  getAll(): Observable<Result<ListBlogSubscriber[]>> {
    return this.apiService.get<ListBlogSubscriber[]>(this.baseUrl);
  }

  getById(id: string): Observable<Result<BlogSubscriber>> {
    return this.apiService.get<BlogSubscriber>(`${this.baseUrl}/GetById?id=${id}`);
  }

  subscribe(createBlogSubscriber: CreateBlogSubscriber): Observable<Result<ListBlogSubscriber>> {
    return this.apiService.post<ListBlogSubscriber>(this.baseUrl, createBlogSubscriber);
  }

  unsubscribe(id: string): Observable<Result> {
    return this.apiService.delete(`${this.baseUrl}?id=${id}`)
  }

}
