import { Component, OnInit } from '@angular/core';
import { environment } from 'projects/admin/src/environments/environment';
import { Language } from 'projects/shared/src/lib/data/localization/language';
import { LanguageSearch } from 'projects/shared/src/lib/data/localization/language-search';
import { LanguagesService } from 'projects/shared/src/lib/services/api/languages/languages.service';

@Component({
  selector: 'app-language-selector',
  templateUrl: './language-selector.component.html',
  styleUrls: ['./language-selector.component.scss']
})
export class LanguageSelectorComponent implements OnInit {

  languages: Language[] = [];
  selectedLanguage: Language | null = null;
  baseApiUrl = environment.baseApiUrl

  constructor(private readonly languagesService: LanguagesService) { }

  ngOnInit(): void {
    this.loadLanguages();
    this.languagesService.reloadAvailableLanguages().subscribe(result => this.languages = result.data.data);
  }

  loadLanguages(): void {
    this.languagesService.availableLanguages.subscribe((availableLanguages) => {
      this.languages = availableLanguages;
      this.selectedLanguage = this.languages[0];
    })
  }

  switchSelectedLanguage(language: Language): void {
    if(this.selectedLanguage !== language) this.selectedLanguage = language;
  }

}
