import { Component, OnInit } from '@angular/core';
import { MessageSettings } from 'projects/shared/src/lib/data/settings/message-settings';
import { SettingsService } from 'projects/shared/src/lib/services/api/settings/settings.service';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { NotificationService } from '../../../services/notification/notification.service';

@Component({
  selector: 'app-message-settings',
  templateUrl: './message-settings.component.html',
  styleUrls: ['./message-settings.component.scss']
})
export class MessageSettingsComponent implements OnInit {

  model: MessageSettings = { confirmationEmailSubjectTemplate: '', confirmationEmailTemplate: '', isSendConfirmationEmail: false, isSendSiteOwnerEmail: false, siteOwnerSubjectTemplate: '', siteOwnerTemplate: '' }
  form: any;
  error: string | undefined;

  constructor(private readonly breadcrumbsService: BreadcrumbsService, private readonly settingsService: SettingsService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadSettings();

    this.breadcrumbsService.setBreadcrumb([
      this.breadcrumbsService.homeCrumb,
      {
        name: 'Settings',
        url: undefined,
        active: true
      },
      {
        name: 'Message Settings',
        url: undefined,
        active: true
      }
    ]);
  }

  loadSettings(): void {
    this.settingsService.getMessageSettings().subscribe((result) => {
      if(result.succeeded) this.model = result.data;
    })
  }

  save(): void {
    this.settingsService.saveMessageSettings(this.model).subscribe((result) => {
      if (result.succeeded) this.notificationService.success('Saved the message settings');
    });
  }

}
