import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { Url } from 'projects/shared/src/lib/data/url';
import { ApiService } from '../api.service';
import { UserPreferences } from '../../../data/user/user-preferences';
import { UpdateUserLanguage } from '../../../data/user-preferences/update-user-language';
import { UpdateUserTheme } from '../../../data/user-preferences/update-user-theme';

@Injectable({
  providedIn: 'root'
})
export class UserPreferencesService {

  constructor(private readonly apiService: ApiService) { }

  updateSelectedLanguage(languageId: Number): Observable<Result<UserPreferences>>{
    const data: UpdateUserLanguage = { languageId: languageId }
    return this.apiService.post<UserPreferences>("UserPreferences/UpdateSelectedLanguage", data);
  }

  updateIsUseDarkMode(isUseDarkMode: Boolean){
    const data: UpdateUserTheme = { isUseDarkMode: isUseDarkMode}
    return this.apiService.post<UserPreferences>("UserPreferences/UpdateIsUseDarkMode", data);
  }

}