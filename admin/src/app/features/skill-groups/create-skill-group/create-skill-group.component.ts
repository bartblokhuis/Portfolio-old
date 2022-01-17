import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CreateUpdateSkillGroup } from 'src/app/data/skill-groups/create-update-skill-group';
import { CreateSkillGroupCreatedEvent } from 'src/app/data/skill-groups/events/create-skill-group-created-event';
import { SkillGroupsService } from 'src/app/services/api/skill-groups/skill-groups.service';

declare var $: any;
@Component({
  selector: 'app-create-skill-group',
  templateUrl: './create-skill-group.component.html',
  styleUrls: ['./create-skill-group.component.scss']
})
export class CreateSkillGroupComponent implements OnInit {

  @Output() onCreated: EventEmitter<CreateSkillGroupCreatedEvent | undefined> = new EventEmitter();

  model: CreateUpdateSkillGroup = { title: '', displayNumber: 0, id: 0 }
  form: any;

  error: string | undefined;
  
  constructor(private skillGroupsService: SkillGroupsService) { }

  ngOnInit(): void { 
    this.form = $('#createForm');
    this.validaitonRules();
  }

  validaitonRules(): void{
    this.form.validate({
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

  cancel(): void {
    this.onCreated.emit();
  }

  save(openNewSkillModal: boolean): void {
    if(!this.form.valid()) return;
    
    this.skillGroupsService.create(this.model).subscribe((result) => {
      const event: CreateSkillGroupCreatedEvent = { skillGroup: result.data, openNewSkillModal: openNewSkillModal};
      this.onCreated.emit(event);
    }, error => this.error = error);
  }

}
