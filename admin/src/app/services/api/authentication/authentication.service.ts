import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { ApiService } from '../api.service';
import { User } from 'src/app/data/user/user';
import { Result } from 'src/app/data/common/Result';
import { LoginResponse } from 'src/app/data/user/response';
import { UserDetails } from 'src/app/data/user/user-details';
import { ChangeUserPassword } from 'src/app/data/user/change-password';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentUserSubject: BehaviorSubject<User> | undefined;
  public currentUser: Observable<User> | undefined;
  
  constructor(private apiService: ApiService) { 

    let localStorageUser = localStorage.getItem('currentUser');
    if(localStorageUser){
      this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorageUser));
      this.currentUser = this.currentUserSubject.asObservable();
    }
  }

  public get currentUserValue(): User | undefined {
    return this.currentUserSubject?.value;
  }

  login(username: string, password: string, rememberMe: boolean): Observable<Result<LoginResponse>> {
    return this.apiService.post<any>('login', {username, password, rememberMe})
      .pipe(map(result => {

        //If we dont have a token it means the login request failed.
        if(!result.succeeded || !result.data.token) return result;

        const user: User = { username: username, expiration: result.data.expiration, id: result.data.userId, token: result.data.token }
        localStorage.setItem('currentUser', JSON.stringify(user));

        if(!this.currentUserSubject) this.currentUserSubject = new BehaviorSubject<User>(user);
        else this.currentUserSubject?.next(user)
        
        return result;
      }))
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject = undefined;
  }

  getUserDetails(): Observable<Result<UserDetails>> {
    return this.apiService.get<UserDetails>("user/details");
  }

  updateUserDetails(userDetails: UserDetails): Observable<Result> {
    return this.apiService.put("user/details", userDetails)
  }

  updatePassword(changePassword: ChangeUserPassword): Observable<Result> {
    return this.apiService.put('user/updatePassword', changePassword);
  }

}
