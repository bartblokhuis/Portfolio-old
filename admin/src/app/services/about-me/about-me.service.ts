import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AboutMe } from '../../data/AboutMe';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class AboutMeService {

  constructor(private apiService: ApiService) {
   }

   getAboutMe(): Observable<AboutMe> {
    return this.apiService.get<AboutMe>(`AboutMe`);
   }

   saveAboutMe(formData: any): Observable<AboutMe> {
     return this.apiService.post<AboutMe>(`AboutMe`, formData);
   }
}
