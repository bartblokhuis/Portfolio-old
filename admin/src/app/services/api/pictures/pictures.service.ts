import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Picture } from 'src/app/data/common/picture';
import { Result } from 'src/app/data/common/Result';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class PicturesService {

  constructor(private apiService: ApiService) { }

  getAll(): Observable<Result<Picture[]>> {
    return this.apiService.get<Picture[]>("Picture");
  }

  delete(pictureId: number): Observable<Result> {
    return this.apiService.delete(`Picture?pictureId=${pictureId}`)
  }
}
