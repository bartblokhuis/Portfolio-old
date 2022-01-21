import { Injectable } from '@angular/core';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { SkillGroup } from 'projects/shared/src/lib/data/skill-groups/skill-group';
import { Skill } from 'projects/shared/src/lib/data/skills/skill';
import { Observable } from 'rxjs';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class SkillsService {
  
  constructor(private apiService: ApiService) { }

  get() : Observable<Result<Skill[]>> {
    return this.apiService.get<Skill[]>('Skill');
  }

  getSkillGroups(): Observable<Result<SkillGroup[]>> {
    return this.apiService.get<SkillGroup[]>('SkillGroup');
  }
}
