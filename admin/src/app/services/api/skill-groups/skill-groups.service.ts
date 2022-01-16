import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from 'src/app/data/common/Result';
import { SkillGroup } from 'src/app/data/skill-groups/skill-group';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class SkillGroupsService {

  constructor(private apiService: ApiService) { }

  getAll(): Observable<Result<SkillGroup[]>> {
    return this.apiService.get<SkillGroup[]>('SkillGroup');
  }
}
