import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Picture } from 'projects/shared/src/lib/data/common/picture';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class PicturesService {

  constructor(private apiService: ApiService) { }

  getAll(): Observable<Result<Picture[]>> {
    return this.apiService.get<Picture[]>("Picture");
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
