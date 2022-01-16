import { Component, OnInit } from '@angular/core';
import { Result } from 'src/app/data/common/Result';
import { EmailSettings } from 'src/app/data/settings/email-settings';
import { ApiService } from 'src/app/services/api/api.service';
import { BreadcrumbsService } from 'src/app/services/breadcrumbs/breadcrumbs.service';
import { NotificationService } from 'src/app/services/notification/notification.service';

declare var $: any;
@Component({
  selector: 'app-email-settings',
  templateUrl: './email-settings.component.html',
  styleUrls: ['./email-settings.component.scss']
})
export class EmailSettingsComponent implements OnInit {

  model: EmailSettings = { displayName: '', email: '', enableSsl: false, host: '', password: '', port: 0, sendTestEmailTo: '', useDefaultCredentials: false, username: '', siteOwnerEmailAddress: ''}
  private url = "Settings/EmailSettings";
  private form: any;
  error: string | undefined;

  constructor(private readonly BreadcrumbService: BreadcrumbsService, private apiService: ApiService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.form = $("#email-settings-form");
    this.validate();
    this.BreadcrumbService.setBreadcrumb([
      {
        name: 'Settings',
        path: undefined,
        active: true
      },
      {
        name: 'Email Settings',
        path: undefined,
        active: true
      }
    ]);

    this.apiService.get<EmailSettings>(this.url).subscribe((result: Result<EmailSettings>) => {
      if(result.succeeded) this.model = result.data;
    })
  }

  validate() {
    this.form.validate({
      rules: {
        email: {
          required: true,
          email: true,
        },
        displayName: {
          required: true
        },
        host: {
          required: true
        },
        port: {
          required: true
        },
        sendTestEmailTo: {
          required: true,
          email: true,
        },
        siteOwnerEmailAddress: {
          required: true,
          email: true
        }
      },
      messages: {
        email: {
          required: "Please enter a email address",
          email: "Please enter a valid email address"
        },
        displayName: {
          required: "Please enter a display name"
        },
        host: {
          required: "Please enter the email server host"
        },
        port: {
          required: "Please enter the email server port"
        },
        sendTestEmailTo: {
          required: "Please enter a test email address",
          email: "Please enter a valid email address"
        },
        siteOwnerEmailAddress: {
          required: "Please enter your email address",
          email: "Please enter a valid email address"
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
    this.apiService.post(this.url, this.model).subscribe((result) => {
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