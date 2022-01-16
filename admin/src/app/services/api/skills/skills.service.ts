import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from 'src/app/data/common/Result';
import { CreateSkill } from 'src/app/data/skills/create-skill';
import { EditSkill } from 'src/app/data/skills/edit-skill';
import { Skill } from 'src/app/data/skills/skill';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class SkillsService {

  constructor(private apiService: ApiService) { }

  create(skill: CreateSkill) {
    return this.apiService.post<Skill>("Skill", skill)
  }
  edit(skill: EditSkill): Observable<Result<Skill>> {
    return this.apiService.put<Skill>("Skill", skill);
  }

  saveSkillImage(skillId: number, formData: FormData): Observable<Result<Skill>> {
    return this.apiService.put<Skill>(`Skill/SaveSkillImage/${skillId}`, formData)
  }

  delete(skillId: number): Observable<Result> {
    return this.apiService.delete(`Skill?id=${skillId}`);
  }
}
