import { Component, OnInit } from '@angular/core';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { GeneralSettings } from 'projects/shared/src/lib/data/settings/general-settings';
import { SettingsService } from '../../../services/api/settings/settings.service';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { NotificationService } from '../../../services/notification/notification.service';

@Component({
  selector: 'app-general-settings',
  templateUrl: './general-settings.component.html',
  styleUrls: ['./general-settings.component.scss']
})
export class GeneralSettingsComponent implements OnInit {

  model: GeneralSettings = { callToActionText: '', footerText: '', footerTextBetweenCopyRightAndYear: false, githubUrl: '', landingDescription: '', landingTitle: '', linkedInUrl: '', showContactMeForm: false, showCopyRightInFooter: false, stackOverFlowUrl: ''}
  private form: any;

  constructor(private readonly BreadcrumbService: BreadcrumbsService, private settingsService: SettingsService, private notificationService: NotificationService) { }

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

    this.settingsService.getGeneralSettings().subscribe((result: Result<GeneralSettings>) => {
      if(result.succeeded) this.model = result.data;
    })
  }

  submit(): void {
    this.settingsService.saveGeneralSettings(this.model).subscribe((result) => {
      this.notificationService.success('Saved the changes')
    });
  }

}
