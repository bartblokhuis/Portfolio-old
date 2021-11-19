import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  get<T>(url: string) : Observable<T>{
    return this.http.get<T>(environment.baseApiUrl + url).pipe(catchError(err => {
      console.log('Handling error locally and rethrowing it...', err);
      return throwError(err);
    }));
  }

  post<T>(url: string, formData: any) :any {
    return this.http.post<T>(environment.baseApiUrl + url, formData);
  }
}
