import { Component, HostListener, OnInit } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';
import { GeneralSettings } from './data/GeneralSettings';
import { SeoSettings } from './data/SeoSettings';
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
    this.settingsService.get<SeoSettings>('SeoSettings').toPromise().then(result => {
      this.seoSettings = result;

     this.title.setTitle(this.seoSettings.title);
     this.meta.addTag({ name: 'description', content: this.seoSettings.defaultMetaDescription});
     
     if(this.seoSettings.useTwitterMetaTags){
      this.meta.addTag({ name: 'twitter:title', content: this.seoSettings.title});
      this.meta.addTag({ name: 'twitter:description', content: this.seoSettings.defaultMetaDescription});

     }
     if(this.seoSettings.useOpenGraphMetaTags){
      this.meta.addTag({ name: 'og:title', content: this.seoSettings.title});
      this.meta.addTag({ name: 'og:description', content: this.seoSettings.defaultMetaDescription});
     }

      this.finishedLoading = true;
    });
  }

}
