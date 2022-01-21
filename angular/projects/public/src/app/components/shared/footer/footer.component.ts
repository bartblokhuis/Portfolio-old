import { Component, OnInit } from '@angular/core';
import { GeneralSettings } from 'projects/shared/src/lib/data/settings/general-settings';
import { SettingsService } from 'projects/shared/src/lib/services/api/settings/settings.service';
import { isObservable } from 'rxjs';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  // @Input() generalSettings: GeneralSettings | null = null;

  //Get the current year
  year: number = new Date().getFullYear();
  generalSettings: GeneralSettings | null = null;

  constructor(private settingService: SettingsService) { }

  ngOnInit(): void {
    var response = this.settingService.getGeneralSettings();
    response.subscribe((result) => {
      this.generalSettings = result.data;
    })
  }

}
