import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { SkillsService } from 'projects/shared/src/lib/services/api/skills/skills.service';
import { NotificationService } from 'projects/admin/src/app/services/notification/notification.service';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { SkillGroup } from 'projects/shared/src/lib/data/skill-groups/skill-group';
import { CreateSkill } from 'projects/shared/src/lib/data/skills/create-skill';
import { Skill } from 'projects/shared/src/lib/data/skills/skill';
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
  error: string | null = null;

  constructor(private skillsService: SkillsService, private notificationService: NotificationService) {
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
    this.error = null;
    if(!this.form.valid()) return;

    this.skillsService.create(this.model).subscribe((result: Result<Skill>) => {

      if(this.formData) {
        this.skillsService.saveSkillImage(result.data.id, this.formData).subscribe((resultWithImage: Result<Skill>) => {
          if(result.succeeded) {
            this.notificationService.success("Created the new skill");
            this.modalRef?.close(resultWithImage.data);
          }
          else {
            this.error = result.messages[0];
          }
          
        });
      }
      else {
        if(result.succeeded) {
          this.notificationService.success("Created the new skill");
          this.modalRef?.close(result.data);
        }
        else {
          this.error = result.messages[0];
        }
        
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
