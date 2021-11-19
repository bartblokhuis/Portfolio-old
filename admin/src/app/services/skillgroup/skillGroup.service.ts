import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SkillGroup } from 'src/app/data/SkillGroup';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SkillGroupService {

  constructor(private http: HttpClient) {
   }

   getSkillGroups(): Observable<SkillGroup[]> {
    return this.http.get<SkillGroup[]>(`${environment.baseApiUrl}SkillGroup`);
  }

  createSkillGroup(skillGroup: SkillGroup): Observable<SkillGroup> {
    return this.http.post<SkillGroup>(`${environment.baseApiUrl}SkillGroup`, skillGroup);
  }

  editSkillGroup(updateSkillGroup: SkillGroup): Observable<SkillGroup> {
    return this.http.put<SkillGroup>(`${environment.baseApiUrl}SkillGroup`, updateSkillGroup);
  }

  deleteSkillGroup(skillGroupId: number): Observable<Object> {
    return this.http.delete(`${environment.baseApiUrl}SkillGroup?id=${skillGroupId}`);
  }
}
