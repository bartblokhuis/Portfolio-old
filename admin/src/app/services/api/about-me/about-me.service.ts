import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AboutMe } from 'src/app/data/about-me';
import { Result } from 'src/app/data/common/Result';
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
