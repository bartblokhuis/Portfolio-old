import { Component, OnInit } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';
import { Event as NavigationEvent, NavigationStart, Router } from '@angular/router';
import { GeneralSettings } from 'projects/shared/src/lib/data/settings/general-settings';
import { SeoSettings } from 'projects/shared/src/lib/data/settings/seo-settings';
import { SettingsService } from 'projects/shared/src/lib/services/api/settings/settings.service';
import { combineLatest, isObservable, Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  finishedLoading: boolean = false;
  seoSettings: SeoSettings | null = null;

  constructor(private settingsService: SettingsService, private title: Title, private meta: Meta, private router:Router) { }

  ngOnInit(): void {

    let observables: Observable<any>[] = [];
    
    const seoSettingsRequest = this.settingsService.getSeoSettings();
    if(isObservable(seoSettingsRequest)) {
      observables.push(seoSettingsRequest);
    }

    const generalSettings = this.settingsService.getGeneralSettings();
    if(isObservable(generalSettings)) observables.push(generalSettings);

    let title: string = "";
    combineLatest(observables).subscribe((result) => {
      this.seoSettings = result[0].data;
      if(this.seoSettings) {

        if(this.seoSettings.title) {
          title = this.seoSettings.title;

          this.title.setTitle(this.seoSettings.title);

          if(this.seoSettings.useOpenGraphMetaTags) {
            this.meta.addTag({ name: 'og:title', content: this.seoSettings.title});
           }
           if(this.seoSettings.useTwitterMetaTags){
            this.meta.addTag({ name: 'twitter:title', content: this.seoSettings.title});
           }
        }

        if(this.seoSettings.defaultMetaDescription) {
          this.meta.addTag({ name: 'description', content: this.seoSettings.defaultMetaDescription});

          if(this.seoSettings.useOpenGraphMetaTags) {
            this.meta.addTag({ name: 'og:title', content: this.seoSettings.defaultMetaDescription});
           }
           if(this.seoSettings.useTwitterMetaTags){
            this.meta.addTag({ name: 'twitter:description', content: this.seoSettings.defaultMetaDescription});
           }
        }
      }

      this.finishedLoading = true;
      return;
    }, (error: any) => {
      console.log(error);
    });
    this.router.events.subscribe((event: NavigationEvent) => {
      if(event instanceof NavigationStart) {
        this.title.setTitle(title);
      }
    });

  }

}
