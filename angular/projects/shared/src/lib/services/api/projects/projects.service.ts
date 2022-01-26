import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { AddProjectPicture } from 'projects/shared/src/lib/data/projects/add-project-picture';
import { CreateProjectUrl } from 'projects/shared/src/lib/data/projects/add-project-url';
import { AddUpdateProject } from 'projects/shared/src/lib/data/projects/add-update-project';
import { Project } from 'projects/shared/src/lib/data/projects/project';
import { ProjectPicture } from 'projects/shared/src/lib/data/projects/project-picture';
import { UpdateProjectPicture } from 'projects/shared/src/lib/data/projects/update-project-picture';
import { UpdateProjectSkills } from 'projects/shared/src/lib/data/projects/update-project-skills';
import { Url } from 'projects/shared/src/lib/data/url';
import { ApiService } from '../api.service';
import { AddProject } from '../../../data/projects/add-project';
import { UpdateProject } from '../../../data/projects/update-project';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {

  constructor(private apiService: ApiService) { }

  getAll(): Observable<Result<Project[]>> {
    return this.apiService.get<Project[]>('Project');
  }

  getAllPublished(): Observable<Result<Project[]>> {
    return this.apiService.get<Project[]>('Project/Published');
  }

  getById(id: number): Observable<Result<Project>> {
    return this.apiService.get<Project>(`Project/GetById?id=${id}`)
  }

  getProjectUrlsByProjectId(id: number): Observable<Result<Url[]>> {
    return this.apiService.get<Url[]>(`Project/Url/GetByProjectId?projectId=${id}`)
  }

  getProjectPicturesByProjectId(id: number): Observable<Result<ProjectPicture[]>> {
    return this.apiService.get<ProjectPicture[]>(`Project/Pictures/GetByProjectId?projectId=${id}`)
  }

  createProject(project: AddProject): Observable<Result<Project>> {
    return this.apiService.post<Project>("Project", project)
  }

  createProjectUrl(projectUrl: CreateProjectUrl): Observable<Result> {
    return this.apiService.post("Project/Url/Create", projectUrl)
  }

  createProjectPicture(createProjectPicture: AddProjectPicture): Observable<Result>{
    return this.apiService.post("Project/Pictures", createProjectPicture);
  }

  updateProject(project: UpdateProject) {
    return this.apiService.put<Project>("Project", project);
  }

  updateProjectSkills(updateProjectSkills: UpdateProjectSkills) : Observable<Result> {
    return this.apiService.put("Project/UpdateSkills", updateProjectSkills)
  }

  updateProjectPicture(updateProjectPicture: UpdateProjectPicture): Observable<Result> {
    return this.apiService.put(`Project/Pictures/`, updateProjectPicture);
  }

  deleteProject(id: number): Observable<Result> {
    return this.apiService.delete(`Project?id=${id}`);
  }

  deleteProjectUrl(projectId: number, urlId: number): Observable<Result> {
    return this.apiService.delete(`Project/Url/Delete?projectId=${projectId}&urlId=${urlId}`);
  }

  deleteProjectPicture(projectId: number, pictureId: number): Observable<Result> {
    return this.apiService.delete(`Project/Pictures?projectId=${projectId}&pictureId=${pictureId}`)
  }
}
