import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { SkillGroup } from 'src/app/data/skill-groups/skill-group';
import { ApiService } from 'src/app/services/api/api.service';
import { NotificationService } from 'src/app/services/notification/notification.service';

@Component({
  selector: 'app-delete-skill-group',
  templateUrl: './delete-skill-group.component.html',
  styleUrls: ['./delete-skill-group.component.scss']
})
export class DeleteSkillGroupComponent implements OnInit {

  @Input() modalRef: NgbModalRef | undefined;
  @Input() skillGroup: SkillGroup | undefined;

  constructor(private apiService: ApiService, private notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  close(){
    this.modalRef?.close();
  }

  remove() {
    this.apiService.delete(`SkillGroup?id=${this.skillGroup?.id}`).subscribe(() => {
      this.notificationService.success(`Removed the ${this.skillGroup?.title} skill group`)
      this.modalRef?.close();
    });
  }

}
