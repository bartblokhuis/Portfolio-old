import { Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { ThemingService } from './theming.service';

@Injectable({
  providedIn: 'root'
})
export class BodyService {

  private body : HTMLBodyElement | undefined = undefined;
  private currentTheme: string = '';
  private defaultClasses: string = 'hold-transition sidebar-mini ' + this.currentTheme;
  private hasCustomClasses: boolean = false;

  constructor(private router: Router, private themeService: ThemingService) { }

  OnInit(): void {
    this.currentTheme = this.themeService.theme.value;
    this.body = document.getElementsByTagName('body')[0];
    this.body?.setAttribute('class', this.defaultClasses + this.currentTheme);
    this.body?.classList.add('test')

    this.router.events.forEach((event) => {
      if(event instanceof NavigationEnd && this.hasCustomClasses) {
        this.body?.removeAttribute('class');
        this.body?.setAttribute('class', this.defaultClasses + this.currentTheme);
      }
    });

    this.themeService.theme.subscribe((theme) => {
      if(theme === this.currentTheme) return;

      if(theme === 'dark-mode'){
        this.body?.classList.remove("light-mode");
        this.body?.classList.add(theme);
      }
      else {
        this.body?.classList.remove("dark-mode");
        this.body?.classList.add(theme);
      }
      this.currentTheme = theme;
    })
  }

  setCustomClasses(classes: string) {
    this.body?.removeAttribute('class');
    this.body?.setAttribute('class', classes);
    this.body?.classList.add(this.currentTheme);
    this.hasCustomClasses = true;
  }
}
