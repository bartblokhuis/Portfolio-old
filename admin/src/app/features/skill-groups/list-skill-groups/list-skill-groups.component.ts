import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Result } from 'src/app/data/common/Result';
import { CreateUpdateSkillGroup } from 'src/app/data/skill-groups/create-update-skill-group';
import { CreateSkillGroupCreatedEvent } from 'src/app/data/skill-groups/events/create-skill-group-created-event';
import { ListSkillGroup, SkillGroup } from 'src/app/data/skill-groups/skill-group';
import { Skill } from 'src/app/data/skills/skill';
import { ApiService } from 'src/app/services/api/api.service';
import { DeleteSkillGroupComponent } from '../delete-skill-group/delete-skill-group.component';
import { CreateSkillComponent } from '../skills/create-skill/create-skill.component';

declare var $: any;

@Component({
  selector: 'app-list-skill-groups',
  templateUrl: './list-skill-groups.component.html',
  styleUrls: ['./list-skill-groups.component.scss']
})
export class ListSkillGroupsComponent implements OnInit {

  skillGroups: ListSkillGroup[] | undefined;
  showCreateSkillGroup: boolean = false;

  constructor(private apiService: ApiService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.loadSkillGroups();
  }

  loadSkillGroups(): void {
    this.apiService.get<SkillGroup[]>('SkillGroup').subscribe((result: Result<SkillGroup[]>) => {
      if(result.succeeded) this.skillGroups = result.data.map(s => ({ title: s.title, displayNumber: s.displayNumber, id: s.id, skills: s.skills, inEditMode: false }));
    })
  }
  
  skillGroupCreated($event: CreateSkillGroupCreatedEvent | undefined): void {
    this.showCreateSkillGroup = false;

    if(!$event) return; //User has canceled creating a new skill group

    this.skillGroups?.push(new ListSkillGroup($event.skillGroup));
    if($event.openNewSkillModal) {
      const modalRef = this.modalService.open(CreateSkillComponent, { size: 'lg' })
        modalRef.componentInstance.skillGroup = $event.skillGroup;
        modalRef.componentInstance.modalRef = modalRef;

        modalRef.result.then(((result: Skill) => {
        
        if(!result) return;
        $event.skillGroup.skills.push(result)
      }))
      .catch((error) => {
        console.log(`ran into error: ${error}`)
      });
    }
  }

  edit(skillGroupId: number): void {

    if(!this.skillGroups) return;

    const skillGroupIndex = this.skillGroups?.findIndex((skillGroup => skillGroup.id == skillGroupId));
    this.skillGroups[skillGroupIndex].inEditMode = true;
  }

  cancelEdit(skillGroupId: number): void {
    if(!this.skillGroups) return;

    const skillGroupIndex = this.skillGroups?.findIndex((skillGroup => skillGroup.id == skillGroupId));
    this.skillGroups[skillGroupIndex].inEditMode = false;
  }

  saveEdit(skillGroupId: number): void {
    const editTitle = <HTMLInputElement>document.getElementById('edit-title-' + skillGroupId);
    const editForm = $('#edit-form-' + skillGroupId);

    if(!editForm || !editTitle) return;

    //Form validation
    this.editValidaitonRules(editForm);
    if(!editForm.valid()) return;

    const editSkillGroup: CreateUpdateSkillGroup = { displayNumber: 0, id: skillGroupId, title: editTitle.value };
    this.apiService.put<SkillGroup>('SkillGroup', editSkillGroup).subscribe((result) => {
      if(!this.skillGroups || !result.succeeded) return;

      const skillGroupIndex = this.skillGroups.findIndex((skillGroup => skillGroup.id == skillGroupId));
      this.skillGroups[skillGroupIndex] = new ListSkillGroup(result.data);
    });
  }

  editValidaitonRules(form: any): void{
    form.validate({
      rules: {
        title: {
          required: true,
        },
      },
      messages: {
        title: {
          required: "Please enter a title"
        }
      },
      errorElement: 'span',
      errorPlacement: function (error: any, element: any) {
        error.addClass('invalid-feedback');
        element.closest('.form-group').append(error);
      },
      highlight: function (element: any, errorClass: any, validClass: any) {
        $(element).addClass('is-invalid');
      },
      unhighlight: function (element: any, errorClass: any, validClass: any) {
        $(element).removeClass('is-invalid');
      }
    });
  }

  delete(skillGroup: SkillGroup) : void {
    this.openModal(DeleteSkillGroupComponent, skillGroup);
  }

  openModal(component: any, skillGroup: SkillGroup | undefined = undefined){

    const modalRef = this.modalService.open(component, { size: 'lg' });

    if(skillGroup) {
      const modalSkillGroup: SkillGroup = { title: skillGroup.title, 
        displayNumber: skillGroup.displayNumber, id: skillGroup.id, skills: skillGroup.skills };
        
      modalRef.componentInstance.skillGroup = modalSkillGroup;
    }
    modalRef.componentInstance.modalRef = modalRef;
    
    modalRef.result.then(() => {
      this.loadSkillGroups();
    });
  }
}
