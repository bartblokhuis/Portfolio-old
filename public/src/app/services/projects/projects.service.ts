import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from 'src/app/data/common/result';
import { Project } from 'src/app/data/project/Project';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {

  constructor(private apiService: ApiService) { }

  get() : Observable<Result<Project[]>> {
    return this.apiService.get<Project[]>('Project/Published');
  }
}
