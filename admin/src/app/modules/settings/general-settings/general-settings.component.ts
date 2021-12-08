import { Component, OnInit } from '@angular/core';
import { GeneralSettings } from 'src/app/data/settings/general-settings';
import { ApiService } from 'src/app/services/api/api.service';
import { BreadcrumbsService } from 'src/app/services/breadcrumbs/breadcrumbs.service';
import { NotificationService } from 'src/app/services/notification/notification.service';

@Component({
  selector: 'app-general-settings',
  templateUrl: './general-settings.component.html',
  styleUrls: ['./general-settings.component.scss']
})
export class GeneralSettingsComponent implements OnInit {

  model: GeneralSettings = { callToActionText: '', footerText: '', footerTextBetweenCopyRightAndYear: false, githubUrl: '', landingDescription: '', landingTitle: '', linkedInUrl: '', showContactMeForm: false, showCopyRightInFooter: false, stackOverFlowUrl: ''}
  private url = "Settings/GeneralSettings";
  private form: any;

  constructor(private readonly BreadcrumbService: BreadcrumbsService, private apiService: ApiService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.BreadcrumbService.setBreadcrumb([
      {
        name: 'Settings',
        path: undefined,
        active: true
      },
      {
        name: 'General Settings',
        path: undefined,
        active: true
      }
    ]);

    this.apiService.get<GeneralSettings>(this.url).subscribe((result: GeneralSettings) => {
      this.model = result;
    })
  }

  submit(): void {
    this.apiService.post(this.url, this.model).subscribe((result) => {
      this.notificationService.success('Saved the changes')
    });
  }

}
