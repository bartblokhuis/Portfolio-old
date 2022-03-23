import { ApplicationRef, Injectable } from '@angular/core';
import { User } from 'projects/shared/src/lib/data/user/user';
import { AuthenticationService } from 'projects/shared/src/lib/services/api/authentication/authentication.service';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ThemingService {

  themes = ["dark-mode", "light-mode"]; // <- list all themes in this array
  theme = new BehaviorSubject("light-theme"); // <- initial theme
  private currentUser: User | null = null;

  constructor(private ref: ApplicationRef, private readonly authenticationService: AuthenticationService) {
  }

  initialize(){
    this.currentUser = this.authenticationService.currentUser;

    // Initially check if dark mode is enabled on system
    let darkModeOn =
      window.matchMedia &&
      window.matchMedia("(prefers-color-scheme: dark)").matches;

    if(this.currentUser && this.currentUser.userPreferences && this.currentUser.userPreferences.isUseDarkMode != null){
      darkModeOn = this.currentUser.userPreferences.isUseDarkMode;
    }
    
    // If dark mode is enabled then directly switch to the dark-theme
    if(darkModeOn){
      console.log(this.currentUser, "CURRENT USER");
      this.theme.next("dark-mode");
    }

    // Watch for changes of the preference
    window.matchMedia("(prefers-color-scheme: dark)").addListener(e => {
      const turnOn = e.matches;
      this.theme.next(turnOn ? "dark-mode" : "light-mode");

      // Trigger refresh of UI
      this.ref.tick();
    });
  }
}
