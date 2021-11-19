import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CreateUpdateSkill, Skill } from 'src/app/data/Skill';
import { SkillGroup } from 'src/app/data/SkillGroup';
import { SkillService } from 'src/app/services/skills/skill.service';

@Component({
  selector: 'app-edit-skill',
  templateUrl: './edit-skill.component.html',
  styleUrls: ['./edit-skill.component.scss']
})
export class EditSkillComponent implements OnInit {

  @Input() skillGroup: SkillGroup;
  @Input() modalRef: NgbModalRef;
  @Input() skill: Skill

  currentFileName: string = 'File';

  editForm = new FormGroup({
    name: new FormControl('test'),
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required])
  });

  constructor(private skillService: SkillService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.editForm.controls.name.setValue(this.skill.name);
  }

  onFileChange(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.currentFileName = file.name;
      this.editForm.patchValue({
        fileSource: file
      });
    }
  }

  submit(){

    const skill: CreateUpdateSkill = {
      id: this.skill.id,
      displayNumber: this.skill.displayNumber,
      iconPath: this.skill.iconPath,
      skillGroupId: this.skillGroup.id,
      name: this.editForm.get('name').value
    };

    this.skillService.editSkill(skill).subscribe((skill) =>{
      const fileSource = this.editForm.get('fileSource').value;
      
      if(!fileSource) {
        this.modalRef.close();
        this.toastr.success("Saved skill: " + skill.name)
        return;
      }

      const formData = new FormData();
      formData.append('icon', fileSource);

      this.skillService.saveSkillImage(skill.id, formData).subscribe(() => {
        this.modalRef.close();
        this.toastr.success("Saved skill: " + skill.name)
      });
    })
  }

}
