import { Injectable } from '@angular/core';
import { AboutMe } from 'projects/shared/src/lib/data/about-me';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { Observable } from 'rxjs';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class AboutMeService {

  constructor(private apiService: ApiService) { }

  get() : Observable<Result<AboutMe>> {
    return this.apiService.get<AboutMe>('AboutMe');
  }

}
