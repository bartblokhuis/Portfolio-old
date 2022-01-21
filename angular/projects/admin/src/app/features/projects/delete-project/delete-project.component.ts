import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Project } from 'projects/shared/src/lib/data/projects/project';
import { ProjectsService } from '../../../services/api/projects/projects.service';
import { NotificationService } from '../../../services/notification/notification.service';

@Component({
  selector: 'app-delete-project',
  templateUrl: './delete-project.component.html',
  styleUrls: ['./delete-project.component.scss']
})
export class DeleteProjectComponent implements OnInit {

  @Input() modalRef: NgbModalRef | undefined;
  @Input() project: Project | undefined;

  constructor(private projectsService: ProjectsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
  }

  close(){
    this.modalRef?.close();
  }

  remove() {
    if(!this.project) return;
    
    this.projectsService.deleteProject(this.project?.id).subscribe(() => {
      this.notificationService.success("Removed the project")
      this.modalRef?.close();
    });
  }

}
