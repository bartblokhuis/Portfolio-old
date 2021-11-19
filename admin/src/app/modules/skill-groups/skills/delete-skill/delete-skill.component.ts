import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Skill } from 'src/app/data/Skill';
import { SkillService } from 'src/app/services/skills/skill.service';

@Component({
  selector: 'app-delete-skill',
  templateUrl: './delete-skill.component.html',
  styleUrls: ['./delete-skill.component.scss']
})
export class DeleteSkillComponent implements OnInit {

  @Input() skill: Skill;
  @Input() modalRef: NgbModalRef;

  constructor(private skillService: SkillService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  close(){
    this.modalRef.close();
  }

  remove(id: number){
    this.skillService.deleteSkill(id).subscribe(result => {
      this.modalRef.close();
      this.toastr.success("Removed skill: " + this.skill.name)
    });
  }

}
