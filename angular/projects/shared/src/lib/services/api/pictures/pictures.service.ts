import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Picture } from 'projects/shared/src/lib/data/common/picture';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { ApiService } from '../api.service';
import { PictureList } from '../../../data/pictures/picture-list';
import { BaseSearchModel } from '../../../data/common/base-search-model';

@Injectable({
  providedIn: 'root'
})
export class PicturesService {

  constructor(private apiService: ApiService) { }

  getAll(): Observable<Result<Picture[]>> {
    return this.apiService.get<Picture[]>("Picture");
  }

  list(searchModel: BaseSearchModel): Observable<Result<PictureList>> {
    return this.apiService.post("Picture/List", searchModel);
  }

  create(url: string, formData: FormData): Observable<Result<Picture>> {
    return this.apiService.post<Picture>(url, formData)
  }

  edit(url: string, formData: FormData): Observable<Result<Picture>> {
    return this.apiService.put<Picture>(url, formData)
  }

  delete(pictureId: number): Observable<Result> {
    return this.apiService.delete(`Picture?pictureId=${pictureId}`)
  }
}
