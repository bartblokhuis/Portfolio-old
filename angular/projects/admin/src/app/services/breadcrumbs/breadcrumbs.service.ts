import { Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { BreadcrumbItem } from 'projects/shared/src/lib/data/breadcrumb-item';

@Injectable({
  providedIn: 'root'
})
export class BreadcrumbsService {

  private breadcrumb: BreadcrumbItem[] = [];
  breadcrumbItems:BehaviorSubject<BreadcrumbItem[]> = new BehaviorSubject(this.breadcrumb);

  constructor(router: Router) { 
    
    router.events.forEach((event) => {

      if(event instanceof NavigationEnd) {

        var url = router.url.replace("/", "");
        if(url.indexOf("-") !== -1){
          url = url.replace("-", " ")
        }
        if(url.indexOf("/") !== -1){
          url = url.replace("/", " ")
        }

        url = url.charAt(0).toUpperCase() + url.slice(1);
        this.breadcrumbItems.next([{name: url, path: undefined, active: true}]);
      }
    });
  }

  setBreadcrumb(breadcrumbItems: BreadcrumbItem[]) {
    this.breadcrumbItems.next(breadcrumbItems);
  }
}
