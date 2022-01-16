import { Component, OnInit } from '@angular/core';
import { Result } from 'src/app/data/common/Result';
import { SeoSettings } from 'src/app/data/settings/seo-settings';
import { ApiService } from 'src/app/services/api/api.service';
import { SettingsService } from 'src/app/services/api/settings/settings.service';
import { BreadcrumbsService } from 'src/app/services/breadcrumbs/breadcrumbs.service';
import { ContentTitleService } from 'src/app/services/content-title/content-title.service';
import { NotificationService } from 'src/app/services/notification/notification.service';

@Component({
  selector: 'app-seo-settings',
  templateUrl: './seo-settings.component.html',
  styleUrls: ['./seo-settings.component.scss']
})
export class SeoSettingsComponent implements OnInit {

  model: SeoSettings = { title: '', defaultMetaDescription: '', defaultMetaKeywords: '', useOpenGraphMetaTags: false, useTwitterMetaTags: false}

  constructor(private readonly BreadcrumbService: BreadcrumbsService, private readonly contentTitleService: ContentTitleService, private settingsService: SettingsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.BreadcrumbService.setBreadcrumb([
      {
        name: 'Settings',
        path: undefined,
        active: true
      },
      {
        name: 'SEO Settings',
        path: undefined,
        active: true
      }
    ]);

    this.contentTitleService.title.next("SEO Settings");

    this.settingsService.getSeoSettings().subscribe((result: Result<SeoSettings>) => {
      if(result.succeeded) this.model = result.data;
    })
  }


  submit() : void {
    this.settingsService.saveSeoSettings(this.model).subscribe((result) => {
      this.notificationService.success('Saved the changes')
    });
  }

}
