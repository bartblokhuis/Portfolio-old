import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Result } from 'src/app/data/common/Result';

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
