import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) {
  }

  get<T>(url: string) : Observable<T>{
    return this.http.get<T>(environment.baseApiUrl + url).pipe(catchError(err => {
      console.log('Handling error locally and rethrowing it...', err);
      return throwError(err);
    }));
  }

  errorMsg: string;
  post<T>(url: string, formData: any) :any {
    return this.http.post<T>(environment.baseApiUrl + url, formData).pipe(
      catchError(error => {
          if (error.error instanceof ErrorEvent) {
              this.errorMsg = `Error: ${error.error.message}`;
          } else {
              this.errorMsg = `Error: ${error.message}`;
          }
          console.log(error, this.errorMsg, "test")
          return of([]);
      })
  );
  }

  handleError(error: HttpErrorResponse) {
    console.log(error);
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      // Client-side errors
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side errors
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
}
