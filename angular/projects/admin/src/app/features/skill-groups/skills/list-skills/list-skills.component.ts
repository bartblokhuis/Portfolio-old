import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { environment } from 'projects/admin/src/environments/environment';
import { SkillGroup } from 'projects/shared/src/lib/data/skill-groups/skill-group';
import { Skill } from 'projects/shared/src/lib/data/skills/skill';
import { CreateSkillComponent } from '../create-skill/create-skill.component';
import { DeleteSkillComponent } from '../delete-skill/delete-skill.component';
import { EditSkillComponent } from '../edit-skill/edit-skill.component';

@Component({
  selector: 'app-list-skills',
  templateUrl: './list-skills.component.html',
  styleUrls: ['./list-skills.component.scss']
})
export class ListSkillsComponent implements OnInit {

  @Input() skillGroup: SkillGroup;
  baseUrl = environment.baseApiUrl;
  
  constructor(private modalService: NgbModal) { 
    this.skillGroup = { title: '', displayNumber: 0, id: 0, skills: [] }
  }

  ngOnInit(): void {
  }

  addSkill(): void {
    const modalRef = this.modalService.open(CreateSkillComponent, { size: 'lg' })
     modalRef.componentInstance.skillGroup = this.skillGroup;
     modalRef.componentInstance.modalRef = modalRef;

     modalRef.result.then(((result: Skill) => {
       
      if(!result) return;
      this.skillGroup.skills.push(result)
    }))
    .catch((error) => {
      console.log(`ran into error: ${error}`)
    });
  }

  editSkill(skill: Skill) {

    const skillIndex = this.skillGroup.skills.findIndex(x => x.id == skill.id);

    const modalRef = this.modalService.open(EditSkillComponent, { size: 'lg' })
     modalRef.componentInstance.skillGroup = this.skillGroup;
     modalRef.componentInstance.skill = skill;
     modalRef.componentInstance.modal = modalRef;

     modalRef.result.then(((result: Skill) => {

      if(!this.skillGroup.skills || !result) return;
      this.skillGroup.skills[skillIndex] = result;
    }))
    .catch((error) => {
      console.log(`ran into error: ${error}`)
    });
  }

  deleteSkill(skill: Skill) {
    const modalRef = this.modalService.open(DeleteSkillComponent, { size: 'lg' })
     modalRef.componentInstance.skill = skill;
     modalRef.componentInstance.modal = modalRef;

     modalRef.result.then((result => {

      if(result === 'removed'){
        this.skillGroup.skills = this.skillGroup.skills.filter(x => x.id !== skill.id);
      }
    }))
    .catch((error) => {
      console.log(`ran into error: ${error}`)
    });
  }

}
