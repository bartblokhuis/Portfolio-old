import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { SkillsService } from 'projects/shared/src/lib/services/api/skills/skills.service';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { EditSkill } from 'projects/shared/src/lib/data/skills/edit-skill';
import { Skill } from 'projects/shared/src/lib/data/skills/skill';
import { validateSkillForm } from '../helpers/skill-helpers';
import { NotificationService } from 'projects/admin/src/app/services/notification/notification.service';

declare var $: any;
@Component({
  selector: 'app-edit-skill',
  templateUrl: './edit-skill.component.html',
  styleUrls: ['./edit-skill.component.scss']
})
export class EditSkillComponent implements OnInit {

  @Input() modal: NgbModalRef | undefined;
  @Input() skill: Skill | undefined;

  model: EditSkill = { displayNumber: 0, iconPath: '', id: 0, name: '', skillGroupId: 0}
  form: any;

  currentFileName = "";
  formData: FormData | undefined;
  error: string | null = null;

  constructor(private skillsService: SkillsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.currentFileName = this.skill?.iconPath ?? "";

    if(!this.skill) return;

    this.model = { displayNumber: this.skill.displayNumber, iconPath: this.skill.iconPath, id: this.skill.id, name: this.skill.name, skillGroupId: this.skill.skillGroupId };

    this.form = $("#editSkillForm");
    validateSkillForm(this.form);
  }

  submit() : void {
    this.error = null;
    if(!this.form.valid()) return;

    this.skillsService.edit(this.model).subscribe((result: Result<Skill>) => {

      if(this.formData) {
        this.skillsService.saveSkillImage(result.data.id, this.formData).subscribe((resultWithImage: Result<Skill>) => {
          if(result.succeeded) {
            this.notificationService.success("Update the skill");
            this.modal?.close(resultWithImage.data);
          }
          else{
            this.error = result.messages[0];
          }
        });
      }
      else {
        if(result.succeeded) {
          this.notificationService.success("Update the skill");
          this.modal?.close(result.data);
        } 
        else{
          this.error = result.messages[0];
        }
      }
    }, error => this.error = error);
  }

  onFileChange($event: any) {
    if ($event.target.files.length > 0) {
      const file = $event.target.files[0];
      this.currentFileName = file.name;
      this.formData = new FormData();
      this.formData.append('icon', file);
    }
  }

}
