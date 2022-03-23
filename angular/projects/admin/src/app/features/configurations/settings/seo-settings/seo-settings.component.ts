import { Component, OnInit } from '@angular/core';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { SeoSettings } from 'projects/shared/src/lib/data/settings/seo-settings';
import { SettingsService } from 'projects/shared/src/lib/services/api/settings/settings.service';
import { BreadcrumbsService } from '../../../../services/breadcrumbs/breadcrumbs.service';
import { ContentTitleService } from '../../../../services/content-title/content-title.service';
import { NotificationService } from '../../../../services/notification/notification.service';

@Component({
  selector: 'app-seo-settings',
  templateUrl: './seo-settings.component.html',
  styleUrls: ['./seo-settings.component.scss']
})
export class SeoSettingsComponent implements OnInit {

  model: SeoSettings = { title: '', defaultMetaDescription: '', defaultMetaKeywords: '', useOpenGraphMetaTags: false, useTwitterMetaTags: false}

  constructor(private readonly breadcrumbsService: BreadcrumbsService, private readonly contentTitleService: ContentTitleService, private settingsService: SettingsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.breadcrumbsService.setBreadcrumb([
      this.breadcrumbsService.homeCrumb,
      {
        name: 'Settings',
        url: undefined,
        active: true
      },
      {
        name: 'SEO Settings',
        url: undefined,
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
