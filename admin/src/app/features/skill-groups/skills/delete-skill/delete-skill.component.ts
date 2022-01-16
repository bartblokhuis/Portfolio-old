import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Skill } from 'src/app/data/skills/skill';
import { ApiService } from 'src/app/services/api/api.service';
import { NotificationService } from 'src/app/services/notification/notification.service';

@Component({
  selector: 'app-delete-skill',
  templateUrl: './delete-skill.component.html',
  styleUrls: ['./delete-skill.component.scss']
})
export class DeleteSkillComponent implements OnInit {

  @Input() modal: NgbModalRef | undefined;
  @Input() skill: Skill | undefined;

  constructor(private apiService: ApiService, private notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  remove(): void {

    if(!this.skill) return;

    this.apiService.delete(`Skill?id=${this.skill.id}`).subscribe(() => {
      this.notificationService.success(`Removed skill: ${this.skill?.name}`)
      this.modal?.close("removed");
    });
  }

}
