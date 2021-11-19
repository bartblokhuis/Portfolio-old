import { Component, OnInit, Input, Inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Skill } from 'src/app/data/Skill';
import { SkillGroup } from 'src/app/data/SkillGroup';
import { CreateSkillComponent } from '../create-skill/create-skill.component';
import { SkillService } from '../../../../services/skills/skill.service';
import { EditSkillComponent } from '../edit-skill/edit-skill.component';
import { DeleteSkillComponent } from '../delete-skill/delete-skill.component';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-list-skill',
  templateUrl: './list-skill.component.html',
  styleUrls: ['./list-skill.component.scss']
})
export class ListSkillComponent implements OnInit {

  @Input() skillGroup: SkillGroup;

  constructor(private modalService: NgbModal, private skillService: SkillService) { }

  baseUrl = environment.baseApiUrl;

  ngOnInit(): void {
  }

  addSkill(): void {
    const modalRef = this.modalService.open(CreateSkillComponent, { size: 'lg' })
     modalRef.componentInstance.skillGroup = this.skillGroup;
     modalRef.componentInstance.modalRef = modalRef;

     modalRef.result.then((result => {
      this.skillService.getSkillsByGroupId(this.skillGroup.id).subscribe((skills: Skill[]) => {
        this.skillGroup.skills = skills;
      })
    }))
    .catch((error) => {
      console.log(`ran into error: ${error}`)
    });
  }

  editSkill(skill: Skill){
    const modalRef = this.modalService.open(EditSkillComponent, { size: 'lg' })
     modalRef.componentInstance.skillGroup = this.skillGroup;
     modalRef.componentInstance.skill = skill;
     modalRef.componentInstance.modalRef = modalRef;

     modalRef.result.then((result => {
      this.skillService.getSkillsByGroupId(this.skillGroup.id).subscribe((skills: Skill[]) => {
        this.skillGroup.skills = skills;
      })
    }))
    .catch((error) => {
      console.log(`ran into error: ${error}`)
    });
  }

  deleteSkill(skill: Skill) {
    const modalRef = this.modalService.open(DeleteSkillComponent, { size: 'lg' })
     modalRef.componentInstance.skill = skill;
     modalRef.componentInstance.modalRef = modalRef;

     modalRef.result.then((result => {
      this.skillService.getSkillsByGroupId(this.skillGroup.id).subscribe((skills: Skill[]) => {
        this.skillGroup.skills = skills;
      })
    }))
    .catch((error) => {
      console.log(`ran into error: ${error}`)
    });
  }
}
