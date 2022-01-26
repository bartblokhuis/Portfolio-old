import { Component, OnInit } from '@angular/core';
import { PublicSiteSettings } from 'projects/shared/src/lib/data/settings/public-site-settings';
import { SettingsService } from 'projects/shared/src/lib/services/api/settings/settings.service';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { ContentTitleService } from '../../../services/content-title/content-title.service';
import { NotificationService } from '../../../services/notification/notification.service';

declare var $: any;
@Component({
  selector: 'app-public-site-settings',
  templateUrl: './public-site-settings.component.html',
  styleUrls: ['./public-site-settings.component.scss']
})
export class PublicSiteSettingsComponent implements OnInit {
  
  model: PublicSiteSettings = { publicSiteUrl: '' }
  form: any;
  error: string | undefined;

  constructor(private readonly breadcrumbsService: BreadcrumbsService, private readonly settingsService: SettingsService, private readonly notificationService: NotificationService, private readonly contentTitleService: ContentTitleService) { }

  ngOnInit(): void {
    this.loadSettings();

    this.form = $("#public-site-settings-form");
    this.validateForm();

    this.contentTitleService.title.next('Public site settings')

    this.breadcrumbsService.setBreadcrumb([
      this.breadcrumbsService.homeCrumb,
      {
        name: 'Settings',
        url: undefined,
        active: true
      },
      {
        name: 'Public site settings',
        url: undefined,
        active: true
      }
    ]);
  }

  loadSettings(): void {
    this.settingsService.getPublicSiteSettings().subscribe((result) => {
      if(result.succeeded) this.model = result.data;
    })
  }

  save(): void {

    if(!this.form.valid()) return;

    this.settingsService.savePublicSiteSettings(this.model).subscribe((result) => {
      if (result.succeeded) this.notificationService.success('Saved the public site settings');
    });
  }

  validateForm(): void {
    this.form.validate({
      rules: {
        publicSiteUrl: {
          required: true,
          url: true,
        }
      },
      messages: {
        publicSiteUrl: {
          required: "Please enter the public site's url",
          url: "Please enter a valid url"
        }
      },
      errorElement: 'span',
      errorPlacement: function (error: any, element: any) {
        error.addClass('invalid-feedback');
        element.closest('.form-group').append(error);
      },
      highlight: function (element: any, errorClass: any, validClass: any) {
        $(element).addClass('is-invalid');
      },
      unhighlight: function (element: any, errorClass: any, validClass: any) {
        $(element).removeClass('is-invalid');
      }
    });
  }

}
