import { Component, Input, OnInit } from '@angular/core';
import { Skill } from 'src/app/data/Skill';
import { SkillGroup } from 'src/app/data/SkillGroup';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-skills',
  templateUrl: './skills.component.html',
  styleUrls: ['./skills.component.scss']
})
export class SkillsComponent implements OnInit {

  @Input() skillGroups: SkillGroup[] | null = [];

  baseUrl = environment.baseApiUrl;

  constructor() { }

  ngOnInit(): void {
  }

}
