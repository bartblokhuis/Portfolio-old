import { DOCUMENT } from '@angular/common';
import { AfterViewInit, Component, HostListener, Inject, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class NavbarComponent implements OnInit, AfterViewInit {

  menuItems: MenuItem[]  = [ {friendlyName: 'HOME', active: false, containerId: 'home', element: null}, { friendlyName: "ABOUT", active: false, containerId: 'about', element: null}, { friendlyName: "MY WORK", active: false, containerId: 'my-work', element: null}, { friendlyName: "SKILLS", active: false, containerId: 'skills', element: null},
  { friendlyName: "CONTACT", active: false, containerId: 'contact', element: null} ];
  navBar: HTMLElement | null = null;
  ignoreScrollEvent: boolean = false;

  constructor(@Inject(DOCUMENT) private document: Document) { }

  @HostListener("window:scroll", []) onWindowScroll() {


    const verticalOffset =
      window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop ||
      0;

      this.scrollEvent(verticalOffset);

    // do some stuff here when the window is scrolled
    //this.scrollEvent();
    this.document.body.style.setProperty("--calc-height", "auto");
    this.resize();
  }

  @HostListener("window:load", []) onWindowLoad() {
    this.resize();
  }

  calcTargets: any = this.document.getElementsByClassName("navbarContent");
  resize = () => {
    for (let target of this.calcTargets) {
      let size = target.firstElementChild.clientHeight + "px";
      if (target.style.getPropertyValue("--calc-height") !== size) {
        target.style.setProperty("--calc-height", size);
      }
    }
  };

  

  ngAfterViewInit(): void {

    this.navBar = this.document.getElementById("navbar");

    this.menuItems.forEach((x) => {
      const element: HTMLElement | null = this.document.getElementById(x.containerId);
      if(element) x.element = element;
    });
  }

  ngOnInit(): void {
  }

  scrollEvent = (verticalOffset: number): void => {
    if(this.ignoreScrollEvent) {
      this.stickyNavBar(verticalOffset);
      return;
    };
    this.checkActiveMenuItem(verticalOffset);
    this.stickyNavBar(verticalOffset);
  }

  stickyNavBar(verticalOffset: number){
    const home = this.menuItems[0];
    if(!this.navBar || !home || !home.element) return;

    //Get the bottom of the home section.
    const homeEnd = home.element.offsetTop + home.element.offsetHeight;
    const navBarHeight = this.getNavBarHeight();

    if(this.navBar.offsetTop - 1 <= verticalOffset && !this.navBar.classList.contains('fixed')){
      this.navBar.classList.add('fixed');
    }

    else if(homeEnd > verticalOffset + navBarHeight && this.navBar.classList.contains('fixed')){
      this.navBar.classList.remove('fixed');
    }
  }

  navigate(menuItem: MenuItem){
    if(!menuItem.element || !this.navBar) return;

    this.ignoreScrollEvent = true;
    let scrollToX = menuItem.element.offsetTop;

    if(this.shouldNavBarBeFixed(scrollToX)) {
      scrollToX -= this.getNavBarHeight();
    }
    
    window.scrollTo({
      top: scrollToX,
      left: 0,
      behavior : "smooth"
    });
    
    var scrollTimeout: any;
    document.addEventListener('scroll', function(e) {
        clearTimeout(scrollTimeout);
        scrollTimeout = setTimeout(function() {
          doneScrolling(e);
        }, 100);
    });

    const doneScrolling = (e: any) => {
      this.checkActiveMenuItem(e);
      this.ignoreScrollEvent = false;
    }
  }

  shouldNavBarBeFixed(scrollToX: number): boolean {
    const home = this.menuItems[0];
    if(!this.navBar || !home || !home.element) return false;
    const homeEnd = home.element.offsetTop + home.element.offsetHeight;
    return homeEnd <= scrollToX;
  }

  checkActiveMenuItem(verticalOffset: number) {
    const home = this.menuItems[0];
    if(!this.navBar || !home || !home.element) return;

    const navBarHeight = this.getFixedNavBarHeight();
    let foundActive = false;
    for(let i = this.menuItems.length - 1; i >= 0; i--){
      
      const item = this.menuItems[i];
      if(!item.element) return;

      if(foundActive){ 
        item.active = false;
      }
      else if(item.element.offsetTop <= verticalOffset + navBarHeight && !foundActive){
        foundActive = true;
        item.active = true;
      }else{
        item.active = false;
      }
    }
    foundActive = false;
  }

  getFixedNavBarHeight(){
    if(!this.navBar) return 0;
    return (this.navBar.classList.contains('fixed')) ? this.navBar.offsetHeight : 0;
  }

  getNavBarHeight(): number {
    if(!this.navBar) return 0;
    return this.navBar.offsetHeight;
  }

  toggleNavBar(): void {
    const ul = document.getElementById("toggleNavBar");
    if(!ul) return;

    if(!ul.classList.contains("open")) {
      ul.classList.add("open");
      ul.classList.remove("collapsed");
    }
    else if (!ul.classList.contains("collapsed")) {

      ul.classList.add("collapsing");
      setTimeout(() => {
        ul.classList.add("collapsed");
        ul.classList.remove("open");
        ul.classList.remove("collapsing");
      }, 350)

     
      
    }
  }
}

interface MenuItem {
  friendlyName: string,
  containerId: string,
  active: boolean,
  element: HTMLElement | null;
}