import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AboutMe } from 'projects/shared/src/lib/data/about-me';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class AboutMeService {

  constructor(private apiService: ApiService) { }

  get(): Observable<Result<AboutMe>> {
    return this.apiService.get<AboutMe>('AboutMe');
  }

  save(aboutMe: AboutMe): Observable<Result<AboutMe>> {
    return this.apiService.post<AboutMe>('AboutMe', aboutMe);
  }
}
