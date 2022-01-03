import { Component, OnInit } from '@angular/core';
import { Skill } from 'src/app/data/Skill';
import { SkillGroup } from 'src/app/data/SkillGroup';
import { SkillsService } from 'src/app/services/skills/skills.service';
import { environment } from 'src/environments/environment';

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