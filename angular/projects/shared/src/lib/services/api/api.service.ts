import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { environment } from 'projects/admin/src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private readonly baseUrl = environment.baseApiUrl;

  constructor(private httpClient: HttpClient) { }

  get<output>(url: string): Observable<Result<output>> {
    return this.httpClient.get<Result<output>>(this.baseUrl + url);
  }

  post<output>(url: string, data: {}): Observable<Result<output>> {
    return this.httpClient.post<Result<output>>(this.baseUrl + url, data);
  }

  put<output>(url: string, data: {}): Observable<Result<output>> {
    return this.httpClient.put<Result<output>>(this.baseUrl + url, data);
  }

  delete<output>(url: string): Observable<any> {
    return this.httpClient.delete(this.baseUrl + url);
  }
}
