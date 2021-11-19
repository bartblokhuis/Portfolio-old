import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AboutMe } from 'src/app/data/AboutMe';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class AboutMeService {

  constructor(private apiService: ApiService) { }

  get() : Observable<AboutMe> {
    return this.apiService.get<AboutMe>('AboutMe');
  }

}
