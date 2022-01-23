import { Component, OnInit } from '@angular/core';
import { BlogSettings } from 'projects/shared/src/lib/data/settings/blog-settings';
import { SettingsService } from 'projects/shared/src/lib/services/api/settings/settings.service';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { NotificationService } from '../../../services/notification/notification.service';

@Component({
  selector: 'app-blog-settings',
  templateUrl: './blog-settings.component.html',
  styleUrls: ['./blog-settings.component.scss']
})
export class BlogSettingsComponent implements OnInit {

  model: BlogSettings = { emailOnPublishingTemplate: '', emailOnSubscribingTemplate: '', isSendEmailOnPublishing: false, isSendEmailOnSubscribing: false, emailOnPublishingSubjectTemplate: '', emailOnSubscribingSubjectTemplate: '' }
  form: any;
  error: string | undefined;

  constructor(private readonly BreadcrumbService: BreadcrumbsService, private readonly settingsService: SettingsService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadSettings();

    this.BreadcrumbService.setBreadcrumb([
      {
        name: 'Settings',
        path: undefined,
        active: true
      },
      {
        name: 'Blog Settings',
        path: undefined,
        active: true
      }
    ]);
  }

  loadSettings(): void {
    this.settingsService.getBlogSettings().subscribe((result) => {
      if(result.succeeded) this.model = result.data;
    })
  }

  save(): void {
    this.settingsService.saveBlogSettings(this.model).subscribe((result) => {
      if (result.succeeded) this.notificationService.success('Saved the blog settings');
    });
  }
}
