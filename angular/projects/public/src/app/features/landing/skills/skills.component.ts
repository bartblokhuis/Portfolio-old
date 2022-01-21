import { Component, OnInit } from '@angular/core';
import { environment } from 'projects/admin/src/environments/environment';
import { SkillGroup } from 'projects/shared/src/lib/data/skill-groups/skill-group';
import { SkillGroupsService } from 'projects/shared/src/lib/services/api/skill-groups/skill-groups.service';

@Component({
  selector: 'app-skills',
  templateUrl: './skills.component.html',
  styleUrls: ['./skills.component.scss']
})
export class SkillsComponent implements OnInit {

  skillGroups: SkillGroup[] | null = null;
  baseUrl = environment.baseApiUrl;

  constructor(private skillGroupService: SkillGroupsService) { }

  ngOnInit(): void {
    this.skillGroupService.getAll().subscribe((result) => {
      if(result.succeeded) this.skillGroups = result.data;
    });
  }

  calcDelay(index: number): number {
    return index * 50;
  }
}