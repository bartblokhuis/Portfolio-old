import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Project } from 'src/app/data/projects/project';
import { ApiService } from 'src/app/services/api/api.service';
import { NotificationService } from 'src/app/services/notification/notification.service';

@Component({
  selector: 'app-delete-project',
  templateUrl: './delete-project.component.html',
  styleUrls: ['./delete-project.component.scss']
})
export class DeleteProjectComponent implements OnInit {

  @Input() modalRef: NgbModalRef | undefined;
  @Input() project: Project | undefined;

  constructor(private apiService: ApiService, private notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  close(){
    this.modalRef?.close();
  }

  remove() {
    this.apiService.delete(`Project?id=${this.project?.id}`).subscribe(() => {
      this.notificationService.success("Removed the project")
      this.modalRef?.close();
    });
  }

}
