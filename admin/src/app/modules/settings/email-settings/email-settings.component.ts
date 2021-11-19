import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { EmailSettings } from 'src/app/data/EmailSettings';

import { SettingserviceService } from '../../../services/settings/settingservice.service';

@Component({
  selector: 'app-email-settings',
  templateUrl: './email-settings.component.html',
  styleUrls: ['./email-settings.component.scss']
})
export class EmailSettingsComponent implements OnInit {

  showSaveButton = false;

  emailSettingsForm = new FormGroup({
    email: new FormControl(''),
    displayName: new FormControl(''),
    host: new FormControl(''),
    port: new FormControl(''),
    username: new FormControl(''),
    password: new FormControl(''),
    enableSsl: new FormControl(''),
    useDefaultCredentials: new FormControl(''),
    sendTestEmailTo: new FormControl(''),
  });

  constructor(private settngsService: SettingserviceService, private toastrService: ToastrService) { }

   // convenience getter for easy access to form fields
   get f() { return this.emailSettingsForm.controls; }

  ngOnInit(): void {

    this.settngsService.get<EmailSettings>("EmailSettings").subscribe((settings) => {
      this.f.email.setValue(settings.email);
      this.f.displayName.setValue(settings.displayName);
      this.f.host.setValue(settings.host);
      this.f.port.setValue(settings.port);
      this.f.username.setValue(settings.username);
      this.f.password.setValue(settings.password);
      this.f.enableSsl.setValue(settings.enableSsl);
      this.f.useDefaultCredentials.setValue(settings.useDefaultCredentials);
      this.f.sendTestEmailTo.setValue(settings.sendTestEmailTo);
    });

  }

  changedSettings(): void {
    this.showSaveButton = true;
  }

  saveEmailSettings(): void {
    var settings: EmailSettings = {
      email: this.f.email.value,
      displayName: this.f.displayName.value,
      enableSsl: this.f.enableSsl.value,
      host: this.f.host.value,
      password: this.f.password.value,
      port: this.f.port.value,
      sendTestEmailTo: this.f.sendTestEmailTo.value,
      useDefaultCredentials: this.f.useDefaultCredentials.value ?? false,
      username: this.f.username.value ?? false
    };

    this.settngsService.save<EmailSettings>(settings, "EmailSettings").subscribe(() => {
      this.toastrService.success("Saved email settings");
      this.showSaveButton = false;
    });
  }

}
