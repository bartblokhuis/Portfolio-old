import { Injectable } from '@angular/core';
import { Result } from 'projects/admin/src/app/data/common/Result';
import { Observable } from 'rxjs';
import { AboutMe } from '../../data/AboutMe';
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
