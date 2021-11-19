import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { GeneralSettings } from 'src/app/data/GeneralSettings';

import { SettingserviceService } from '../../../services/settings/settingservice.service';


@Component({
  selector: 'app-general-settings',
  templateUrl: './general-settings.component.html',
  styleUrls: ['./general-settings.component.scss']
})
export class GeneralSettingsComponent implements OnInit {

  showSaveButton = false;

  seoSettingsForm = new FormGroup({
    landingTitle: new FormControl(''),
    landingDescription: new FormControl(''),
    callToActionText: new FormControl(''),
    linkedInUrl: new FormControl(''),
    githubUrl: new FormControl(''),
    stackOverFlowUrl: new FormControl(''),
    footerText: new FormControl(''),
    showCopyRightInFooter: new FormControl(''),
    footerTextBetweenCopyRightAndYear: new FormControl(''),
    showContactMeForm: new FormControl(''),
  });

  constructor(private settingsService: SettingserviceService, private toastrService: ToastrService) { }

  get f() { return this.seoSettingsForm.controls; }

  ngOnInit(): void {

    this.settingsService.get<GeneralSettings>("GeneralSettings").subscribe((settings) => {
      this.f.landingTitle.setValue(settings.landingTitle);
      this.f.landingDescription.setValue(settings.landingDescription);
      this.f.callToActionText.setValue(settings.callToActionText);
      this.f.linkedInUrl.setValue(settings.linkedInUrl);
      this.f.githubUrl.setValue(settings.githubUrl);
      this.f.stackOverFlowUrl.setValue(settings.stackOverFlowUrl);
      this.f.footerText.setValue(settings.footerText);
      this.f.showCopyRightInFooter.setValue(settings.showCopyRightInFooter);
      this.f.footerTextBetweenCopyRightAndYear.setValue(settings.footerTextBetweenCopyRightAndYear);
      this.f.showContactMeForm.setValue(settings.showContactMeForm);
    });

  }

  changedSettings(): void {
    this.showSaveButton = true;
  }

  saveSeoSettings(): void {

    var settings: GeneralSettings = {
      landingTitle: this.f.landingTitle.value,
      landingDescription: this.f.landingDescription.value,
      callToActionText: this.f.callToActionText.value,
      linkedInUrl: this.f.linkedInUrl.value ?? false,
      githubUrl: this.f.githubUrl.value ?? false,
      stackOverFlowUrl: this.f.stackOverFlowUrl.value ?? false,
      footerText: this.f.footerText.value ?? false,
      showCopyRightInFooter: this.f.showCopyRightInFooter.value ?? false,
      footerTextBetweenCopyRightAndYear: this.f.footerTextBetweenCopyRightAndYear.value ?? false,
      showContactMeForm: this.f.showContactMeForm.value ?? false,
    };

    this.settingsService.save<GeneralSettings>(settings, "GeneralSettings").subscribe(() => {
      this.toastrService.success("Saved general settings");
      this.showSaveButton = false;
    });
  }


}
