import { HttpErrorResponse } from '@angular/common/http';
import { Component, HostListener, OnInit } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';
import { combineLatest, isObservable, Observable } from 'rxjs';
import { GeneralSettings } from './data/settings/GeneralSettings';
import { SeoSettings } from './data/settings/SeoSettings';
import { SettingsService } from './services/settings/settings.service';

declare var AOS: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  finishedLoading: boolean = false;
  seoSettings: SeoSettings | null = null;

  @HostListener("window:load", []) onWindowLoad() {
    if(AOS) AOS.init();
  }

  constructor(private settingsService: SettingsService, private title: Title, private meta: Meta) { }

  ngOnInit(): void {

    let observables: Observable<any>[] = [];
    
    const seoSettingsRequest = this.settingsService.get<SeoSettings>('SeoSettings');
    if(isObservable(seoSettingsRequest)) {
      observables.push(seoSettingsRequest);
    }

    const generalSettings = this.settingsService.get<GeneralSettings>("GeneralSettings");
    if(isObservable(generalSettings)) observables.push(generalSettings);

    combineLatest(observables).subscribe((result) => {
      console.log(result);
      this.seoSettings = result[0];
      if(this.seoSettings) {

        if(this.seoSettings.title) {
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
  }

}
