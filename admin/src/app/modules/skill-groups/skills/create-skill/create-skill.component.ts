import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CreateUpdateSkill } from 'src/app/data/Skill';
import { SkillGroup } from 'src/app/data/SkillGroup';
import { SkillService } from 'src/app/services/skills/skill.service';

@Component({
  selector: 'app-create-skill',
  templateUrl: './create-skill.component.html',
  styleUrls: ['./create-skill.component.scss']
})
export class CreateSkillComponent implements OnInit {

  @Input() skillGroup: SkillGroup;
  @Input() modalRef: NgbModalRef;

  currentFileName: string = 'File';

  createForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required])
  });

  constructor(private skillService: SkillService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onFileChange(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.currentFileName = file.name;
      this.createForm.patchValue({
        fileSource: file
      });
    }
  }

  submit(){

    const skill: CreateUpdateSkill = {
      id: 0,
      displayNumber: 0,
      iconPath: '',
      skillGroupId: this.skillGroup.id,
      name: this.createForm.get('name').value
    };

    this.skillService.createSkill(skill).subscribe((skill) => {
      const skillId = skill.id;

      const formData = new FormData();
      formData.append('icon', this.createForm.get('fileSource').value);
      this.skillService.saveSkillImage(skillId, formData).subscribe(() => {
        this.toastr.success("Saved skill: " + skill.name);
        this.modalRef.close();
      });
    });
  }

}
