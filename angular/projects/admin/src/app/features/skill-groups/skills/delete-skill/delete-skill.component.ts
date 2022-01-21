import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { SkillsService } from 'projects/shared/src/lib/services/api/skills/skills.service';
import { NotificationService } from 'projects/admin/src/app/services/notification/notification.service';
import { Skill } from 'projects/shared/src/lib/data/skills/skill';

@Component({
  selector: 'app-delete-skill',
  templateUrl: './delete-skill.component.html',
  styleUrls: ['./delete-skill.component.scss']
})
export class DeleteSkillComponent implements OnInit {

  @Input() modal: NgbModalRef | undefined;
  @Input() skill: Skill | undefined;

  constructor(private skillsService: SkillsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  remove(): void {

    if(!this.skill) return;

    this.skillsService.delete(this.skill.id).subscribe(() => {
      this.notificationService.success(`Removed skill: ${this.skill?.name}`)
      this.modal?.close("removed");
    });
  }

}
