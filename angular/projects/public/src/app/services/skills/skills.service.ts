import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from '../../data/common/result';
import { Skill } from '../../data/Skill';
import { SkillGroup } from '../../data/SkillGroup';
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
