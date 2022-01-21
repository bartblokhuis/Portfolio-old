import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'projects/admin/src/environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Result } from 'projects/shared/src/lib/data/common/Result';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  get<T>(url: string) : Observable<Result<T>>{
    return this.http.get<Result<T>>(environment.baseApiUrl + url).pipe(catchError(err => {
      console.log('Handling error locally and rethrowing it...', err);
      return throwError(err);
    }));
  }

  post<T>(url: string, formData: any): Observable<Result<T>> {
    return this.http.post<Result<T>>(environment.baseApiUrl + url, formData);
  }
}
