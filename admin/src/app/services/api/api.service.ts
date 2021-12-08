import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private readonly baseUrl = environment.baseApiUrl;

  constructor(private httpClient: HttpClient) { }

  get<output>(url: string): Observable<output> {
    return this.httpClient.get<output>(this.baseUrl + url);
  }

  post<output>(url: string, data: {}): Observable<output> {
    return this.httpClient.post<output>(this.baseUrl + url, data);
  }

  put<output>(url: string, data: {}): Observable<output> {
    return this.httpClient.put<output>(this.baseUrl + url, data);
  }

  delete<output>(url: string): Observable<any> {
    return this.httpClient.delete(this.baseUrl + url);
  }
}
