import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private apiService: ApiService) { }

  get<setting>(url: string): Observable<setting> {
    return this.apiService.get<setting>(`Settings/${url}`);
  }
}
