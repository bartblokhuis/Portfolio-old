import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from 'src/app/data/common/Result';
import { CreateUpdateSkillGroup } from 'src/app/data/skill-groups/create-update-skill-group';
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

  create(model: CreateUpdateSkillGroup): Observable<Result<SkillGroup>>{
    return this.apiService.post<SkillGroup>('SkillGroup', model)
  }

  edit(model: CreateUpdateSkillGroup): Observable<Result<SkillGroup>> {
    return this.apiService.put<SkillGroup>('SkillGroup', model)
  }

  delete(id: number) {
    return this.apiService.delete(`SkillGroup?id=${id}`);
  }
}
