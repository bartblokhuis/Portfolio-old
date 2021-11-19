import { DOCUMENT } from '@angular/common';
import { Component, ElementRef, Inject, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SkillGroup } from 'src/app/data/SkillGroup';
import { SkillGroupService } from '../../../services/skillgroup/skillgroup.service';
import { DeleteSkillGroupComponent } from '../delete-skill-group/delete-skill-group.component';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { CreateSkillComponent } from '../skills/create-skill/create-skill.component';

@Component({
  selector: 'app-list-skill-group',
  templateUrl: './list-skill-group.component.html',
  styleUrls: ['./list-skill-group.component.scss']
})
export class ListSkillGroupComponent implements OnInit {

  skillGroups: SkillGroup[];
  showCreateSkillGroup: boolean = false;

  constructor(private modalService: NgbModal, private skillGroupService: SkillGroupService, @Inject(DOCUMENT) private document) { }

  ngOnInit(): void {
    this.loadSkillGroups();
  }

  loadSkillGroups(): void{
    this.skillGroupService.getSkillGroups().subscribe((skillGroups) => {
      this.skillGroups = skillGroups;
    });
  }

  deleteSkillGroup(skillGroup: SkillGroup){
    const modalRef = this.modalService.open(DeleteSkillGroupComponent, { size: 'lg' });
    modalRef.componentInstance.skillGroup = skillGroup;
    modalRef.componentInstance.modalRef = modalRef;

    modalRef.result.then((result => {
      this.loadSkillGroups();
    }))
    .catch((error) => {
      console.log(`ran into error: ${error}`)
    });
  }

  createSkillGroup(): void {
    this.showCreateSkillGroup = true;
  }

  createdSkillGroup (skillGroup: SkillGroup, openNewSkillModal: boolean) : void {
    if(openNewSkillModal){
      const modalRef = this.modalService.open(CreateSkillComponent, { size: 'lg' })
      modalRef.componentInstance.skillGroup = skillGroup;
      modalRef.componentInstance.modalRef = modalRef;

      modalRef.result.then(() => {
        this.showCreateSkillGroup = false;
        this.loadSkillGroups();
      });
    }
    else{
      this.showCreateSkillGroup = false;
      this.loadSkillGroups();
    }
  }

  readonly editButtonId : string = "_edit_button";
  readonly labelId : string = "_label";
  readonly editId : string = "_edit";
  readonly deleteButtonId : string = "_delete";

  editSkillGroup(skillGroupId: number){
    
    this.show(skillGroupId, this.editId);
    this.hide(skillGroupId, this.labelId);
    this.hide(skillGroupId, this.editButtonId);
    this.hide(skillGroupId, this.deleteButtonId);

  }

  saveSkillGroup(skillGroup: SkillGroup): void {

    const newTitle = this.document.getElementById(skillGroup.id + "_input").value;
    if(skillGroup.title === newTitle) {
      this.hide(skillGroup.id, this.editId);
      this.show(skillGroup.id, this.labelId);
      this.show(skillGroup.id, this.editButtonId);
      this.show(skillGroup.id, this.deleteButtonId);
      return;
    }


    skillGroup.title = newTitle;
    this.skillGroupService.editSkillGroup(skillGroup).subscribe(() => {
      // this.hideSkillGroupEditField(skillGroup.id);
      // this.hideSkillGroupSaveIcon(skillGroup.id);
    });
  }

  hide(skillGroupId: number, id: string){
    const inputEl = this.document.getElementById(skillGroupId + id);
    inputEl.style.display = 'none';
  }

  show(skillGroupId: number, id: string){
    const inputEl = this.document.getElementById(skillGroupId + id);
    inputEl.style.display = '';
  }

}
