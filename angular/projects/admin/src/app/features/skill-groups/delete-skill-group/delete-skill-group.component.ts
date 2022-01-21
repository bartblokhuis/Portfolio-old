import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { SkillGroup } from '../../../data/skill-groups/skill-group';
import { SkillGroupsService } from '../../../services/api/skill-groups/skill-groups.service';
import { NotificationService } from '../../../services/notification/notification.service';

@Component({
  selector: 'app-delete-skill-group',
  templateUrl: './delete-skill-group.component.html',
  styleUrls: ['./delete-skill-group.component.scss']
})
export class DeleteSkillGroupComponent implements OnInit {

  @Input() modalRef: NgbModalRef | undefined;
  @Input() skillGroup: SkillGroup | undefined;

  constructor(private skillGroupsService: SkillGroupsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  close(){
    this.modalRef?.close();
  }

  remove() {
    if(!this.skillGroup) return;
    
    this.skillGroupsService.delete(this.skillGroup.id).subscribe(() => {
      this.notificationService.success(`Removed the ${this.skillGroup?.title} skill group`)
      this.modalRef?.close();
    });
  }

}
