import { Component, OnInit } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';

@Component({
  selector: 'page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.scss']
})
export class PageHeaderComponent implements OnInit {

  header: string = '';

  constructor(private router: Router) {
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

        this.header = url
      }
    });
  }

  ngOnInit(): void {
  }

}
