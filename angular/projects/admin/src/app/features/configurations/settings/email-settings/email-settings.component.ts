import { Component, OnInit } from '@angular/core';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { EmailSettings } from 'projects/shared/src/lib/data/settings/email-settings';
import { SettingsService } from 'projects/shared/src/lib/services/api/settings/settings.service';
import { BreadcrumbsService } from '../../../../services/breadcrumbs/breadcrumbs.service';
import { NotificationService } from '../../../../services/notification/notification.service';

declare var $: any;
@Component({
  selector: 'app-email-settings',
  templateUrl: './email-settings.component.html',
  styleUrls: ['./email-settings.component.scss']
})
export class EmailSettingsComponent implements OnInit {

  model: EmailSettings = { displayName: '', email: '', enableSsl: false, host: '', password: '', port: 0, sendTestEmailTo: '', useDefaultCredentials: false, username: '', siteOwnerEmailAddress: ''}
  private form: any;
  error: string | undefined;

  constructor(private readonly breadcrumbsService: BreadcrumbsService, private settingsService: SettingsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.form = $("#email-settings-form");
    this.validate();
    this.breadcrumbsService.setBreadcrumb([
      this.breadcrumbsService.homeCrumb,
      {
        name: 'Settings',
        url: undefined,
        active: true
      },
      {
        name: 'Email Settings',
        url: undefined,
        active: true
      }
    ]);

    this.settingsService.getEmailSettings().subscribe((result: Result<EmailSettings>) => {
      if(result.succeeded) this.model = result.data;
    })
  }

  validate() {
    this.form.validate({
      rules: {
        email: {
          required: true,
          email: true,
          maxlength: 128
        },
        displayName: {
          required: true,
          maxlength: 64
        },
        host: {
          required: true,
          maxlength: 128
        },
        port: {
          required: true
        },
        sendTestEmailTo: {
          required: true,
          email: true,
          maxlength: 128
        },
        siteOwnerEmailAddress: {
          required: true,
          email: true,
          maxlength: 128
        }
      },
      messages: {
        email: {
          required: "Please enter a email address",
          email: "Please enter a valid email address",
          maxlength: "Please use an email with less than 128 characters"
        },
        displayName: {
          required: "Please enter a display name",
          maxlength: "Please use a name with less than 64 characters"
        },
        host: {
          required: "Please enter the email server host",
          maxlength: "Please use a host with less than 128 characters"
        },
        port: {
          required: "Please enter the email server port"
        },
        sendTestEmailTo: {
          required: "Please enter a test email address",
          email: "Please enter a valid email address",
          maxlength: "Please use an email with less than 128 characters"
        },
        siteOwnerEmailAddress: {
          required: "Please enter your email address",
          email: "Please enter a valid email address",
          maxlength: "Please use an email with less than 128 characters"
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

  submit() : void {
    this.error = undefined;
    if(!this.form.valid()) return;
    this.settingsService.saveEmailSettings(this.model).subscribe((result) => {
      if(!result.succeeded){
        this.error = result.messages[0];
        this.notificationService.warning("Could not update the settings")
      }
      else {
        this.notificationService.success('Saved the changes');
      }
      
    });
  }

}
