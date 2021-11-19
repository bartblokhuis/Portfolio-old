import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Skill } from 'src/app/data/Skill';
import { SkillGroup } from 'src/app/data/SkillGroup';
import { ApiService } from '../common/api.service';

@Injectable({
  providedIn: 'root'
})
export class SkillsService {
  
  constructor(private apiService: ApiService) { }

  get() : Observable<Skill[]> {
    return this.apiService.get<Skill[]>('Skill');
  }

  getSkillGroups(): Observable<SkillGroup[]> {
    return this.apiService.get<SkillGroup[]>('SkillGroup');
  }
}
