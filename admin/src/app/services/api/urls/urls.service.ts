import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from 'src/app/data/common/Result';
import { Url } from 'src/app/data/url';
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
