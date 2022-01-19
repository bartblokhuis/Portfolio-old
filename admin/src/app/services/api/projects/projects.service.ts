import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from 'src/app/data/common/Result';
import { AddUpdateProject } from 'src/app/data/projects/add-update-project';
import { Project } from 'src/app/data/projects/project';
import { UpdateProjectSkills } from 'src/app/data/projects/update-project-skills';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {

  constructor(private apiService: ApiService) { }

  getAll(): Observable<Result<Project[]>> {
    return this.apiService.get<Project[]>('Project');
  }

  getById(id: number): Observable<Result<Project>> {
    return this.apiService.get<Project>(`Project/GetById?id=${id}`)
  }

  createProject(project: AddUpdateProject): Observable<Result<Project>> {
    return this.apiService.post<Project>("Project", project)
  }

  updateProject(project: AddUpdateProject) {
    return this.apiService.put<Project>("Project", project);
  }

  updateProjectSkills(updateProjectSkills: UpdateProjectSkills) : Observable<Result> {
    return this.apiService.put("Project/UpdateSkills", updateProjectSkills)
  }

  updateDemoImage(projectId: number, formData: FormData): Observable<Result> {
    return this.apiService.put(`Project/UpdateDemoImage/${projectId}`, formData);
  }

  deleteProject(id: number): Observable<Result> {
    return this.apiService.delete(`Project?id=${id}`);
  }

  deleteProjectUrl(projectId: number, urlId: number): Observable<Result> {
    return this.apiService.delete(`Project/Url/Delete?projectId=${projectId}&urlId=${urlId}`);
  }
}
