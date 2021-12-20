import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from 'src/app/data/common/result';
import { Skill } from 'src/app/data/Skill';
import { SkillGroup } from 'src/app/data/SkillGroup';
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
