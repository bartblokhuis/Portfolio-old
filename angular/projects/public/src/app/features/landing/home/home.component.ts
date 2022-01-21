import { Component, OnInit } from '@angular/core';
import { GeneralSettings } from 'projects/shared/src/lib/data/settings/general-settings';
import { SettingsService } from 'projects/shared/src/lib/services/api/settings/settings.service';
import { isObservable } from 'rxjs';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  generalSettings: GeneralSettings | null = null;
  
  constructor(private settingService: SettingsService) { }

  ngOnInit(): void {
    this.settingService.getGeneralSettings().subscribe((result) => {
      this.generalSettings = result.data;
    });
  }

  scrollToAboutMe(): void {
    const home = document.getElementById("home");
    if(!home ) return;

    var homeEndsAt = home.clientHeight;

    window.scrollTo({
      top: homeEndsAt + 45,
      left: 0,
      behavior : "smooth"
    });
  }

}
