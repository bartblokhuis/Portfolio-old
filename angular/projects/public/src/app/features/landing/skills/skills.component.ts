import { Component, OnInit } from '@angular/core';
import { environment } from 'projects/admin/src/environments/environment';
import { SkillGroup } from '../../../data/SkillGroup';
import { SkillsService } from '../../../services/skills/skills.service';

@Component({
  selector: 'app-skills',
  templateUrl: './skills.component.html',
  styleUrls: ['./skills.component.scss']
})
export class SkillsComponent implements OnInit {

  skillGroups: SkillGroup[] | null = null;
  baseUrl = environment.baseApiUrl;

  constructor(private skillGroupService: SkillsService) { }

  ngOnInit(): void {
    this.skillGroupService.getSkillGroups().subscribe((result) => {
      if(result.succeeded) this.skillGroups = result.data;
    });
  }

  calcDelay(index: number): number {
    return index * 50;
  }
}