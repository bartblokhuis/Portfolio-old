import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from 'src/app/data/common/Result';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class SystemService {

  constructor(private readonly apiService: ApiService) { }

  clearCache(): Observable<Result> {
    return this.apiService.get("System/ClearCache");
  }
}
