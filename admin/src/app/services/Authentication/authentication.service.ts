import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { ApiService } from '../api/api.service';
import { User } from 'src/app/data/user/user';

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

  login(username: string, password: string, rememberMe: boolean): Observable<any> {
    return this.apiService.post<any>('login', {username, password, rememberMe})
      .pipe(map(result => {

        //If we dont have a token it means the login request failed.
        if(!result.token) return result;

        const user: User = { username: username, expiration: result.expiration, id: result.userId, token: result.token }
        localStorage.setItem('currentUser', JSON.stringify(user));

        console.log(this.currentUserSubject);
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

}
