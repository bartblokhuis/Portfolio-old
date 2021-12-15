import { AfterViewInit, Component, OnInit, ViewEncapsulation } from '@angular/core';

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

  constructor() { }

  ngAfterViewInit(): void {

    this.navBar = document.getElementById("navbar");

    this.menuItems.forEach((x) => {
      const element: HTMLElement | null = document.getElementById(x.containerId);
      if(element) x.element = element;
    });
  }

  ngOnInit(): void {
    window.addEventListener('scroll', this.scrollEvent, true);
  }



  scrollEvent = (event: any): void => {

    if(this.ignoreScrollEvent) {
      this.stickyNavBar(event);
      return;
    };
    this.checkActiveMenuItem(event);
    this.stickyNavBar(event);
  }

  stickyNavBar(event: any){
    const scrollTopVal: number | undefined = parseInt(event.target?.scrollingElement.scrollTop);

    const home = this.menuItems[0];
    if(!this.navBar || !home || !home.element) return;

    //Get the bottom of the home section.
    const homeEnd = home.element.offsetTop + home.element.offsetHeight;
    const navBarHeight = this.getNavBarHeight();

    console.log(this.navBar.offsetTop, scrollTopVal)
    if(this.navBar.offsetTop - 1 <= scrollTopVal && !this.navBar.classList.contains('fixed')){
      this.navBar.classList.add('fixed');
    }

    else if(homeEnd > scrollTopVal + navBarHeight && this.navBar.classList.contains('fixed')){
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
  })
    
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


  checkActiveMenuItem(event: any) {

    const scrollTopVal: number | undefined = parseInt(event.target?.scrollingElement.scrollTop);
    const home = this.menuItems[0];
    if (!scrollTopVal) return;
    if(!this.navBar || !home || !home.element) return;

    const navBarHeight = this.getFixedNavBarHeight();
    let foundActive = false;
    for(let i = this.menuItems.length - 1; i >= 0; i--){
      
      const item = this.menuItems[i];
      if(!item.element) return;

      if(foundActive){ 
        item.active = false;
      }
      else if(item.element.offsetTop <= scrollTopVal + navBarHeight && !foundActive){
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
}

interface MenuItem {
  friendlyName: string,
  containerId: string,
  active: boolean,
  element: HTMLElement | null;
}