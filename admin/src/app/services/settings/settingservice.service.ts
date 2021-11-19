import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SettingserviceService {

  constructor(private http: HttpClient) { }

  get<setting>(url: string): Observable<setting> {
    return this.http.get<setting>(`${environment.baseApiUrl}Settings/${url}`);
  }

  save<setting>(settingData: setting, url: string): Observable<setting> {
    return this.http.post<setting>(`${environment.baseApiUrl}Settings/${url}`, settingData);
  }
}
