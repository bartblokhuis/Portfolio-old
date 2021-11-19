import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Project, UpdateProjectSkills } from 'src/app/data/project';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient) {
   }

   getProjects(): Observable<Project[]> {
     return this.http.get<Project[]>(`${environment.baseApiUrl}Project`);
   }

   createProject(project: Project): Observable<Project> {
     return this.http.post<Project>(`${environment.baseApiUrl}Project`, project);
   }

   updateProject(project: Project): Observable<Project> {
    return this.http.put<Project>(`${environment.baseApiUrl}Project`, project);
  }

   updateProjectSkills(projectSkillsModel: UpdateProjectSkills): Observable<Project> {
     return this.http.put<Project>(`${environment.baseApiUrl}Project/UpdateSkills`, projectSkillsModel);
   }

   updateProjectImage(projectId: number, image: FormData): Observable<Project> {
     return this.http.put<Project>(`${environment.baseApiUrl}Project/UpdateDemoImage/${projectId}`, image);
   }

   deleteProject(projectId: number): Observable<Object> {
    return this.http.delete(`${environment.baseApiUrl}Project?id=${projectId}`);
  }
}
