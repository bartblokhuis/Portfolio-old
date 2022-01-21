import { Injectable } from '@angular/core';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { Project } from 'projects/shared/src/lib/data/projects/project';
import { Observable } from 'rxjs';
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
