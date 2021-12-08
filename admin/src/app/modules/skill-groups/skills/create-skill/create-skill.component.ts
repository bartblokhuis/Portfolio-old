import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { SkillGroup } from 'src/app/data/skill-groups/skill-group';
import { CreateSkill } from 'src/app/data/skills/create-skill';
import { Skill } from 'src/app/data/skills/skill';
import { ApiService } from 'src/app/services/api/api.service';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { validateSkillForm } from '../helpers/skill-helpers';

declare var $: any;
@Component({
  selector: 'app-create-skill',
  templateUrl: './create-skill.component.html',
  styleUrls: ['./create-skill.component.scss']
})
export class CreateSkillComponent implements OnInit {

  @Input() skillGroup: SkillGroup = { displayNumber: 0, id: 0, skills: [], title: '' };
  @Input() modalRef: NgbModalRef | undefined;

  model: CreateSkill = { displayNumber: 0, name: '', skillGroupId: this.skillGroup.id };
  currentFileName: string = '';
  formData: FormData | undefined;
  form: any;
  error: string | undefined;

  constructor(private apiService: ApiService, private notificationService: NotificationService) {
  }

  ngOnInit(): void {
    this.model.skillGroupId = this.skillGroup.id;
    if(this.skillGroup.id === 0) this.modalRef?.close();

    this.form = $("#addSkillForm");
    validateSkillForm(this.form);
  }

  onFileChange($event: any) {
    if ($event.target.files.length > 0) {
      const file = $event.target.files[0];
      this.currentFileName = file.name;
      this.formData = new FormData();
      this.formData.append('icon', file);
    }
  }

  submit() : void {
    if(!this.form.valid()) return;

    this.apiService.post<Skill>("Skill", this.model).subscribe((result: Skill) => {

      if(this.formData) {
        this.apiService.put<Skill>(`Skill/SaveSkillImage/${result.id}`, this.formData).subscribe((resultWithImage: Skill) => {
          this.modalRef?.close(resultWithImage);
        });
      }
      else {
        this.modalRef?.close(result);
      }
    }, error => this.error = error);
  }

  formValidationRules: {} = {
    rules: {
      name: {
        required: true,
      },
      file: {
        accept: "image/jpeg, image/pjpeg, image/png, image/svg+xml, image/tiff, image/webp"
      }
    },
    messages: {
      name: {
        required: "Please enter a title",
      },
      file: {
        accept: "Please enter an image"
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
  };
}
