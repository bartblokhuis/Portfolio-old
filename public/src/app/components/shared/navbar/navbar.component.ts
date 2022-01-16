import { DOCUMENT } from '@angular/common';
import { AfterViewInit, Component, HostListener, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class NavbarComponent implements OnInit, AfterViewInit {

  @HostListener("window:scroll", []) onWindowScroll() {
    this.scrollEvent(this.getVirticalOffset());
    this.document.body.style.setProperty("--calc-height", "auto");
  }

  @HostListener("window:load", []) onWindowLoad() {
    this.resize();
  }

  @HostListener("window:resize", []) onWindowResize() {
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

  menuItems: MenuItem[] = [
    { friendlyName: 'HOME', active: false, containerId: 'home', element: null, path: null}, 
    { friendlyName: "ABOUT", active: false, containerId: 'about', element: null, path: null}, 
    { friendlyName: "MY WORK", active: false, containerId: 'my-work', element: null, path: null}, 
    { friendlyName: "SKILLS", active: false, containerId: 'skills', element: null, path: null},
    { friendlyName: "CONTACT", active: false, containerId: 'contact', element: null, path: null},
    { active: false, containerId: null, element: null, friendlyName: "BLOG", path: '/blog' }
  ];

  navBar: HTMLElement | null = null;
  toggleNavBarElement: HTMLElement | null = null;

  ignoreScrollEvent: boolean = false;

  constructor(private router: Router, @Inject(DOCUMENT) private document: Document) { }

  ngOnInit(): void {
    this.navBar = this.document.getElementById("navbar");
    this.toggleNavBarElement = this.document.getElementById("toggleNavBar");

    if(!this.isOnLandingPage()) {
      this.navBar?.classList.add('fixed')
    }
    else {
      this.navBar?.classList.remove('fixed')
    }
  }

  ngAfterViewInit(): void {
   
    this.menuItems.forEach((x) => {
      if(x.containerId != null){
        const element: HTMLElement | null = this.document.getElementById(x.containerId);
        if(element) x.element = element;
      }
    });
  }

  navigateLandingComponent(menuItem: MenuItem) {
    const home = this.menuItems[0];
    if(!this.navBar) return;

    const isOnLandingPage = this.isOnLandingPage();

    // If the user is not on the landing page we simply redirect to the url.
    if(!isOnLandingPage && menuItem.containerId) {
      this.router.navigate([`/`], {fragment: menuItem.containerId})
      return;
    }
    if(!home || !home.element) return;

    // Scroll the user to the desired part of the landing page.
    if(!menuItem.element) return;

    // Determine the x the user wants to go to.
    // If the navbar is fixed remove the navbar height from the scrollto x to ensure that the navbar isn't hiding content.
    const homeEnd = home.element.offsetTop + home.element.offsetHeight;
    let scrollToX = this.navBar && homeEnd < menuItem.element.offsetTop && homeEnd !== menuItem.element.offsetTop
      ? menuItem.element.offsetTop - this.navBar.offsetHeight
      : menuItem.element.offsetTop;

      if(!this.navBar.classList.contains('fixed') && menuItem.friendlyName !== 'ABOUT'){
        if(menuItem.friendlyName !== 'HOME'){
          scrollToX = scrollToX - this.navBar.offsetHeight - 1;
        }
      }

    // Temporarily disable the scroll event.
    this.ignoreScrollEvent = true;

    if(!menuItem.active) this.deactiveAllMenuItems();

    this.scrollTo(scrollToX, () => {
      menuItem.active = true;
      this.ignoreScrollEvent = false;
      if(this.toggleNavBarElement?.classList.contains("open")) this.toggleNavBar();
    })
  }

  toggleNavBar(): void {
    if(!this.toggleNavBarElement) return;

    if(!this.toggleNavBarElement.classList.contains("open")) {
      this.toggleNavBarElement.classList.add("open");
      this.toggleNavBarElement.classList.remove("collapsed");
    }
    else if (!this.toggleNavBarElement.classList.contains("collapsed")) {

      this.toggleNavBarElement.classList.add("collapsing");
      setTimeout(() => {
        if(!this.toggleNavBarElement) return;
        this.toggleNavBarElement.classList.add("collapsed");
        this.toggleNavBarElement.classList.remove("open");
        this.toggleNavBarElement.classList.remove("collapsing");
      }, 350)
    }
  }

  private scrollEvent(verticalOffset: number) {
    if(!this.isOnLandingPage()) return;

    if(this.ignoreScrollEvent){
      this.stickyNavBar(verticalOffset);
      return;
    }

    this.stickyNavBar(verticalOffset);
    this.checkActiveMenuItem(verticalOffset);
  }

  private stickyNavBar(verticalOffset: number){
    const home = this.menuItems[0];
    if(!this.navBar || !home || !home.element) return;

    //Get the bottom of the home section.
    const homeEnd = home.element.offsetTop + home.element.offsetHeight;

    if(this.navBar.offsetTop - 1 <= verticalOffset && !this.navBar.classList.contains('fixed')){
      this.navBar.classList.add('fixed');
    }

    else if(homeEnd - 1 > verticalOffset && this.navBar.classList.contains('fixed')){
      this.navBar.classList.remove('fixed');
    }
  }

  private checkActiveMenuItem(verticalOffset: number) {
    const home = this.menuItems[0];
    if(!this.navBar || !home || !home.element) return;

    //const navBarHeight = this.getFixedNavBarHeight();
    const navBarHeight = this.navBar && this.navBar.classList.contains('fixed') ? this.navBar.offsetHeight : 0;
    let foundActive = false;

    const menuItems = this.menuItems.filter(menuItem => menuItem.containerId != null);

    for(let i = menuItems.length - 1; i >= 0; i--){
      
      const item = menuItems[i];
      if(!item.element) return;

      if(foundActive){ 
        item.active = false;
      }
      else if(item.element.offsetTop <= verticalOffset + navBarHeight && !foundActive) {
        foundActive = true;
        item.active = true;
      }else{
        item.active = false;
      }
    }
  }

  private deactiveAllMenuItems() : void {
    this.menuItems.forEach(menuItem => {
      menuItem.active = false;
    })
  }

  private isOnLandingPage(): boolean {
    const url = this.router.url;
    return url == "/" || url.startsWith("/#")
  }

  private getVirticalOffset() : number {
    return window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop ||
      0;
  }

  /**
  * Using scroll to with a call back found at:
  * https://stackoverflow.com/questions/52292603/is-there-a-callback-for-window-scrollto
  * Native scrollTo with callback
  * @param offset - offset to scroll to
  * @param callback - callback function
 */
  private scrollTo(offset: number, callback:any) {
    const fixedOffset = offset.toFixed();
    const onScroll = function () {
            if (window.pageYOffset.toFixed() === fixedOffset) {
                window.removeEventListener('scroll', onScroll)
                callback()
            }
        }

    window.addEventListener('scroll', onScroll)
    onScroll()
    window.scrollTo({
        top: offset,
        behavior: 'smooth'
    })
  }

}

interface MenuItem {
  friendlyName: string,
  containerId: string | null,
  active: boolean,
  element: HTMLElement | null;
  path: string | null;
}