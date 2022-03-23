import { Component, OnInit } from '@angular/core';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { UserPreferences } from 'projects/shared/src/lib/data/user/user-preferences';
import { UserPreferencesService } from 'projects/shared/src/lib/services/api/userPreferences/user-preferences.service';
import { Observable } from 'rxjs';
import { NotificationService } from '../../services/notification/notification.service';
import { ThemingService } from '../../services/theming/theming.service';

@Component({
  selector: 'app-theme-switcher',
  templateUrl: './theme-switcher.component.html',
  styleUrls: ['./theme-switcher.component.scss']
})
export class ThemeSwitcherComponent implements OnInit {

  constructor(private readonly themingService: ThemingService, private readonly userPreferencesService: UserPreferencesService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  toggleTheme(): void {
    
    let request: Observable<Result<UserPreferences>> | null = null; 
    if(this.themingService.theme.value === 'dark-mode') {
      this.themingService.theme.next('light-mode');
      request = this.userPreferencesService.updateIsUseDarkMode(false);

    }
    else {
      this.themingService.theme.next('dark-mode');
      request = this.userPreferencesService.updateIsUseDarkMode(true)
    }

    request.subscribe((result) => {
      if(result.succeeded) this.notificationService.success("Updated the preferred theme")
    })
  }

}
