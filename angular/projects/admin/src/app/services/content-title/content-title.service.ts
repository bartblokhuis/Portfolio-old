import { Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ContentTitleService {

  title: BehaviorSubject<string> = new BehaviorSubject('');

  constructor(private router: Router) { 
    router.events.forEach((event) => {

      if(event instanceof NavigationEnd) {

        let url: string = router.url;
        if(router.url.indexOf("/") !== -1){
          url = router.url.substring(router.url.lastIndexOf("/") + 1);
        }

        if(url.indexOf("-") !== -1){
          url = url.replace("-", " ")
        }

        url = url.charAt(0).toUpperCase() + url.slice(1);

        this.title.next(url);
      }
    });
  }
}
