import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateUpdateSkill, Skill } from 'src/app/data/Skill';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SkillService {

  constructor(private http: HttpClient) { }

   getSkills(): Observable<Skill[]> {
     return this.http.get<Skill[]>(`${environment.baseApiUrl}Skill`);
   }

   getSkillsByGroupId(groupId: number): Observable<Skill[]> {
    return this.http.get<Skill[]>(`${environment.baseApiUrl}Skill/GetBySkillGroupId/${groupId}`);
  }

  createSkill(skill: CreateUpdateSkill): Observable<Skill> {
    return this.http.post<Skill>(`${environment.baseApiUrl}Skill`, skill);
  }

  editSkill(skill: CreateUpdateSkill): Observable<Skill> {
    return this.http.put<Skill>(`${environment.baseApiUrl}Skill`, skill);
  }

  saveSkillImage(skillId: number, formData: FormData): Observable<Skill> {
    return this.http.put<Skill>(`${environment.baseApiUrl}Skill/SaveSkillImage/${skillId}`, formData)
  }

  deleteSkill(id: number): Observable<object> {
    return this.http.delete(`${environment.baseApiUrl}Skill?id=${id}`);
  }

}
