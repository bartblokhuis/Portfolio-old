import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from '../../../data/common/Result';
import { EmailSettings } from '../../../data/settings/email-settings';
import { GeneralSettings } from '../../../data/settings/general-settings';
import { SeoSettings } from '../../../data/settings/seo-settings';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private apiService: ApiService) { }

  getEmailSettings(): Observable<Result<EmailSettings>> {
    return this.apiService.get<EmailSettings>("Settings/EmailSettings");
  }

  saveEmailSettings(emailSettings: EmailSettings): Observable<Result> {
    return this.apiService.post("Settings/EmailSettings", emailSettings);
  }

  getGeneralSettings(): Observable<Result<GeneralSettings>> {
    return this.apiService.get<GeneralSettings>("Settings/GeneralSettings");
  }

  saveGeneralSettings(generalSettings: GeneralSettings): Observable<Result> {
    return this.apiService.post("Settings/GeneralSettings", generalSettings);
  }

  getSeoSettings(): Observable<Result<SeoSettings>> {
    return this.apiService.get<SeoSettings>("Settings/SeoSettings");
  }

  saveSeoSettings(seoSettings: SeoSettings): Observable<Result> {
    return this.apiService.post("Settings/SeoSettings", seoSettings);
  }
}
