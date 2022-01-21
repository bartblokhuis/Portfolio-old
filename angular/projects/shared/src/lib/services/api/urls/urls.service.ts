import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { Url } from 'projects/shared/src/lib/data/url';
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
