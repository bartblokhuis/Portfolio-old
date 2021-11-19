import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../../environments/environment';
import { User, UserDetails } from '../../data/User';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
      return this.currentUserSubject.value;
  }

  login(username: string, password: string, rememberMe: boolean) {
    return this.http.post<any>(`${environment.baseApiUrl}login`, { username, password, rememberMe })
        .pipe(map(user => {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify(user));
            this.currentUserSubject.next(user);
            return user;
        }));
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  getUserDetails() : Observable<UserDetails> {
    return this.http.get<UserDetails>(`${environment.baseApiUrl}user/details`);
  }

  updateUserDetails(username: string, email: string, password: string) {
    return this.http.put<any>(`${environment.baseApiUrl}user/details`, {username, email, password})
    .pipe(map(user => {
      // store user details and jwt token in local storage to keep user logged in between page refreshes
      localStorage.removeItem('currentUser');
      localStorage.setItem('currentUser', JSON.stringify(user));
      this.currentUserSubject.next(user);
      return user;
  }));
  }

  updatePassword(password: string, oldPassword: string){
    return this.http.put<any>(`${environment.baseApiUrl}user/updatePassword`, {password, oldPassword})
    .pipe(map(user => {
      // store user details and jwt token in local storage to keep user logged in between page refreshes
      localStorage.removeItem('currentUser');
      localStorage.setItem('currentUser', JSON.stringify(user));
      this.currentUserSubject.next(user);
      return user;
  }));
  }
}
