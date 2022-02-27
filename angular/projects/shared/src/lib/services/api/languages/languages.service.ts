import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from '../../../data/common/Result';
import { Language } from '../../../data/localization/language';
import { LanguageCreate } from '../../../data/localization/language-create';
import { LanguageList } from '../../../data/localization/language-list';
import { LanguageSearch } from '../../../data/localization/language-search';
import { LanguageUpdate } from '../../../data/localization/language-update';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class LanguagesService {

  readonly baseUrl = "Language";

  constructor(private readonly apiService: ApiService) { }

  getById(id: string): Observable<Result<Language>> {
    return this.apiService.get<Language>(`${this.baseUrl}/GetById?id=${id}`);
  }

  search(searchModel: LanguageSearch): Observable<Result<LanguageList>> {
    return this.apiService.post(`${this.baseUrl}/Search`, searchModel);
  }

  create(language: LanguageCreate): Observable<Result<Language>> {
    return this.apiService.post<Language>(`${this.baseUrl}/Create`, language);
  }

  update(language: LanguageUpdate): Observable<Result<Language>> {
    return this.apiService.put<Language>(this.baseUrl, language);
  }

  delete(languageId: number): Observable<Result> {
    return this.apiService.delete(`${this.baseUrl}?languageId=${languageId}`);
  }
}
