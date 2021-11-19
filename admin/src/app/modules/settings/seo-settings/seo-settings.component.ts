import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SeoSettings } from 'src/app/data/SeoSettings';

import { SettingserviceService } from '../../../services/settings/settingservice.service';

@Component({
  selector: 'app-seo-settings',
  templateUrl: './seo-settings.component.html',
  styleUrls: ['./seo-settings.component.scss']
})
export class SeoSettingsComponent implements OnInit {

  showSaveButton = false;

  seoSettingsForm = new FormGroup({
    title: new FormControl(''),
    defaultMetaKeywords: new FormControl(''),
    defaultMetaDescription: new FormControl(''),
    useTwitterMetaTags: new FormControl(''),
    useOpenGraphMetaTags: new FormControl(''),
  });

  constructor(private settingService: SettingserviceService, private toastrService: ToastrService) { }

  get f() { return this.seoSettingsForm.controls; }

  ngOnInit(): void {

    this.settingService.get<SeoSettings>("SeoSettings").subscribe((settings) => {
      this.f.title.setValue(settings.title);
      this.f.defaultMetaKeywords.setValue(settings.defaultMetaKeywords);
      this.f.defaultMetaDescription.setValue(settings.defaultMetaDescription);
      this.f.useTwitterMetaTags.setValue(settings.useTwitterMetaTags);
      this.f.useOpenGraphMetaTags.setValue(settings.useOpenGraphMetaTags);
    });

  }

  changedSettings(): void {
    this.showSaveButton = true;
  }

  saveSeoSettings(): void {

    var settings: SeoSettings = {
      title: this.f.title.value,
      defaultMetaDescription: this.f.defaultMetaDescription.value,
      defaultMetaKeywords: this.f.defaultMetaKeywords.value,
      useOpenGraphMetaTags: this.f.useOpenGraphMetaTags.value ?? false,
      useTwitterMetaTags: this.f.useTwitterMetaTags.value ?? false
    };

    this.settingService.save<SeoSettings>(settings, "SeoSettings").subscribe(() => {
      this.toastrService.success("Saved seo settings");
      this.showSaveButton = false;
    });
  }

}
