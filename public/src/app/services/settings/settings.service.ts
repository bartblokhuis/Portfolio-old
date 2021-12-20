import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  cachedSettings: CachedItem<any>[] = [];

  constructor(private apiService: ApiService) { }

  get<T>(url: string, ): Observable<T> | T {

    let setting = this.cachedSettings.find(x => x.key === url);
    if(setting) {
      return setting.data;
    }

    return this.apiService.get<T>(`Settings/${url}`).pipe(map(result => {
      const cachedItem: CachedItem<T> = { key: url, data: result  };
      this.cachedSettings.push(cachedItem);
      return result;
    }));
  }
}
interface CachedItem<T> {
  key: string,
  data: T
};
