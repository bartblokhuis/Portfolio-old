import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from '../../../data/common/Result';
import { Url } from '../../../data/url';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class UrlsService {

  constructor(private readonly apiService: ApiService) { }

  update(url: Url): Observable<Result<Url>>{
    return this.apiService.put<Url>("Url", url);
  }
}
