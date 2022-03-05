import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
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

  availableLanguages: BehaviorSubject<Language[]> = new BehaviorSubject<Language[]>([]);

  getById(id: string): Observable<Result<Language>> {
    return this.apiService.get<Language>(`${this.baseUrl}/GetById?id=${id}`);
  }

  search(searchModel: LanguageSearch): Observable<Result<LanguageList>> {
    return this.apiService.post(`${this.baseUrl}/Search`, searchModel);
  }

  reloadAvailableLanguages(): Observable<Result<LanguageList>> {
    const searchModel: LanguageSearch = {
      availablePageSizes: '',
      draw: '',
      length: 1,
      page: 0,
      pageSize: 2147483647,
      start: 0,
      onlyShowPublished: true,
    };
    return this.search(searchModel).pipe(map(result => {
      if(result.succeeded &&result.data && result.data.data) this.availableLanguages.next(result.data.data);
      return result
    }));
  }

  create(language: LanguageCreate): Observable<Result<Language>> {
    return this.apiService.post<Language>(`${this.baseUrl}/Create`, language);
  }

  uploadLanguageIcon(languageId: number, formData: FormData): Observable<Result<Language>> {
    return this.apiService.post<Language>(`${this.baseUrl}/${languageId}/uploadLanguageIcon`, formData);
  }

  update(language: LanguageUpdate): Observable<Result<Language>> {
    return this.apiService.put<Language>(this.baseUrl, language);
  }

  delete(languageId: number): Observable<Result> {
    return this.apiService.delete(`${this.baseUrl}?id=${languageId}`);
  }
}
