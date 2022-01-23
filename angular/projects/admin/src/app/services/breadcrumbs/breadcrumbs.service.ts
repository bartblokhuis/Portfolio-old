import { Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { BreadcrumbItem } from 'projects/shared/src/lib/data/breadcrumb-item';
import { SettingsService } from 'projects/shared/src/lib/services/api/settings/settings.service';

@Injectable({
  providedIn: 'root'
})
export class BreadcrumbsService {

  private breadcrumb: BreadcrumbItem[] = [];


  homeCrumb: BreadcrumbItem = {
    active: false,
    name: 'Home',
    url: ''
  }

  breadcrumbItems:BehaviorSubject<BreadcrumbItem[]> = new BehaviorSubject(this.breadcrumb);

  constructor(router: Router, private readonly settingsService: SettingsService) {

    this.settingsService.getPublicSiteSettings().subscribe((result) => {
      this.homeCrumb.url = result.data.publicSiteUrl;
    })
    
    router.events.forEach((event) => {

      if(event instanceof NavigationEnd) {

        var url = router.url.replace("/", "");
        while (url.indexOf("-") !== -1){
          url = url.replace("-", " ")
        }
        while (url.indexOf("/") !== -1){
          url = url.replace("/", " ")
        }

        url = url.charAt(0).toUpperCase() + url.slice(1);
        this.breadcrumbItems.next([this.homeCrumb, {name: url, url: undefined, active: true}]);
      }
    });
  }

  setBreadcrumb(breadcrumbItems: BreadcrumbItem[]) {
    this.breadcrumbItems.next(breadcrumbItems);
  }
}
