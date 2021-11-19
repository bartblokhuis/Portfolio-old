import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Project } from 'src/app/data/Project';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {

  constructor(private apiService: ApiService) { }

  get() : Observable<Project[]> {
    return this.apiService.get<Project[]>('Project');
  }
}
