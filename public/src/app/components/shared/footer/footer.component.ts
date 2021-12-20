import { Component, Input, OnInit } from '@angular/core';
import { isObservable } from 'rxjs';
import { GeneralSettings } from 'src/app/data/settings/GeneralSettings';
import { ApiService } from 'src/app/services/common/api.service';
import { SettingsService } from 'src/app/services/settings/settings.service';

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
    var response = this.settingService.get<GeneralSettings>("GeneralSettings");
    if(!isObservable(response)){
      this.generalSettings = response;
      return;
    }
    
    response.subscribe((result) => {
      this.generalSettings = result;
    })
  }

}
