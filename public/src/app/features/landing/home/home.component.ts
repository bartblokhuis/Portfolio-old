import { Component, OnInit } from '@angular/core';
import { isObservable } from 'rxjs';
import { GeneralSettings } from 'src/app/data/settings/GeneralSettings';
import { SettingsService } from 'src/app/services/settings/settings.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

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
