import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import { AfterViewInit, Component, Inject, OnInit, PLATFORM_ID, ViewEncapsulation } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

declare var AOS: any;

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class LandingComponent implements OnInit, AfterViewInit {

  constructor(private router: Router, @Inject(DOCUMENT) private document: Document, @Inject(PLATFORM_ID) private platformId: Object) { }
  ngAfterViewInit(): void {
    if (isPlatformBrowser(this.platformId)){
      if(AOS) AOS.init();
    }
  }

  // @HostListener("window:load", []) onWindowLoad() {
  //   if(AOS) AOS.init();
  // }

  ngOnInit(): void {
    this.router.events.subscribe(val => {
      if (val instanceof NavigationEnd) {
        let fragmentIdx = val.urlAfterRedirects.lastIndexOf('#');
        if (fragmentIdx >= 0 && fragmentIdx < val.urlAfterRedirects.length - 1) {
          let fragment = val.urlAfterRedirects.substring(fragmentIdx+1);
          
          const elementOfSetTop = this.document.getElementById(fragment)?.offsetTop ?? 0;
          window.scrollTo({
            top: elementOfSetTop,
        })
        }
      }
    })
  }

}
