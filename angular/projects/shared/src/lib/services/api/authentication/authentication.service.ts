import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { ApiService } from '../api.service';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { ChangeUserPassword } from 'projects/shared/src/lib/data/user/change-password';
import { LoginResponse } from 'projects/shared/src/lib/data/user/response';
import { UserToken } from 'projects/shared/src/lib/data/user/user-token';
import { UserDetails } from 'projects/shared/src/lib/data/user/user-details';
import { User } from '../../../data/user/user';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentUserTokenSubject: BehaviorSubject<UserToken> | undefined;
  public currentUserToken: Observable<UserToken> | undefined;

  public currentUser: User | null = null;
  
  constructor(private apiService: ApiService) { 

    let localStorageUser = localStorage.getItem('currentUser');
    if(localStorageUser){
      this.currentUserTokenSubject = new BehaviorSubject<UserToken>(JSON.parse(localStorageUser));
      this.currentUserToken = this.currentUserTokenSubject.asObservable();
    }
  }

  public get currentUserValue(): UserToken | undefined {
    return this.currentUserTokenSubject?.value;
  }

  login(username: string, password: string, rememberMe: boolean): Observable<Result<LoginResponse>> {
    return this.apiService.post<any>('login', {username, password, rememberMe})
      .pipe(map(result => {

        //If we dont have a token it means the login request failed.
        if(!result.succeeded || !result.data.token) return result;

        const user: UserToken = { username: username, expiration: result.data.expiration, id: result.data.userId, token: result.data.token }
        localStorage.setItem('currentUser', JSON.stringify(user));

        if(!this.currentUserTokenSubject) this.currentUserTokenSubject = new BehaviorSubject<UserToken>(user);
        else this.currentUserTokenSubject?.next(user)
        
        return result;
      }))
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserTokenSubject = undefined;
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

  getCurrentUser(): Observable<Result<User>> {
    return this.apiService.get<User>('User/Current').pipe(map(result => {
      this.currentUser = result.data
      return result
    }));
  }

}
