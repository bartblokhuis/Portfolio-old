import { Component, OnInit } from '@angular/core';
import { environment } from 'projects/admin/src/environments/environment';
import { Language } from 'projects/shared/src/lib/data/localization/language';
import { LanguageSearch } from 'projects/shared/src/lib/data/localization/language-search';
import { AuthenticationService } from 'projects/shared/src/lib/services/api/authentication/authentication.service';
import { LanguagesService } from 'projects/shared/src/lib/services/api/languages/languages.service';
import { UserPreferencesService } from 'projects/shared/src/lib/services/api/userPreferences/user-preferences.service';
import { NotificationService } from '../../../services/notification/notification.service';

@Component({
  selector: 'app-language-selector',
  templateUrl: './language-selector.component.html',
  styleUrls: ['./language-selector.component.scss']
})
export class LanguageSelectorComponent implements OnInit {

  languages: Language[] = [];
  selectedLanguage: Language | null = null;
  baseApiUrl = environment.baseApiUrl;

  constructor(private readonly languagesService: LanguagesService, private readonly authenticationService: AuthenticationService, private readonly userPreferencesService: UserPreferencesService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadLanguages();
    this.languagesService.reloadAvailableLanguages().subscribe(result => {
      this.languages = result.data.data;

      const currentUser = this.authenticationService.currentUser;
      if(currentUser && currentUser.userPreferences && currentUser.userPreferences.selectedLanguageId){
        const selectedLanguage = this.languages.filter(x => x.id == currentUser.userPreferences.selectedLanguageId);
        if(selectedLanguage.length > 0){
          this.selectedLanguage = selectedLanguage[0];
          return;
        }
      }
    });
  }

  loadLanguages(): void {
    this.languagesService.availableLanguages.subscribe((availableLanguages) => {
      this.languages = availableLanguages;
      this.selectedLanguage = this.languages[0];
    })
  }

  switchSelectedLanguage(language: Language): void {
    if(this.selectedLanguage !== language) {
      this.selectedLanguage = language;

      this.userPreferencesService.updateSelectedLanguage(language.id).subscribe((result) => {
        if(result.succeeded) this.notificationService.success("Updated the selected language");
        else this.notificationService.error(result.messages[0]);
      });

      //Update selected language in api
    }
  }

}
