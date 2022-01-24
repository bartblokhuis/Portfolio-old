import { Component, OnInit } from '@angular/core';
import { ApiSettings } from 'projects/shared/src/lib/data/settings/api-settings';
import { SettingsService } from 'projects/shared/src/lib/services/api/settings/settings.service';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { NotificationService } from '../../../services/notification/notification.service';

declare var $: any;

@Component({
  selector: 'app-api-settings',
  templateUrl: './api-settings.component.html',
  styleUrls: ['./api-settings.component.scss']
})
export class ApiSettingsComponent implements OnInit {

  model: ApiSettings = { apiUrl: '' }
  form: any;
  error: string | undefined;

  constructor(private readonly breadcrumbsService: BreadcrumbsService, private readonly settingsService: SettingsService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadSettings();

    this.form = $("#api-settings-form");
    this.validateForm();

    this.breadcrumbsService.setBreadcrumb([
      this.breadcrumbsService.homeCrumb,
      {
        name: 'Settings',
        url: undefined,
        active: true
      },
      {
        name: 'API settings',
        url: undefined,
        active: true
      }
    ]);
  }

  loadSettings(): void {
    this.settingsService.getApiSettings().subscribe((result) => {
      if(result.succeeded) this.model = result.data;
    })
  }

  save(): void {

    if(!this.form.valid()) return;

    this.settingsService.saveApiSettings(this.model).subscribe((result) => {
      if (result.succeeded) this.notificationService.success('Saved the API settings');
    });
  }

  validateForm(): void {
    this.form.validate({
      rules: {
        apiUrl: {
          required: true,
        }
      },
      messages: {
        apiUrl: {
          required: "Please enter the API site's url",
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
