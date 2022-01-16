import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Project } from 'src/app/data/projects/project';
import { ProjectsService } from 'src/app/services/api/projects/projects.service';
import { NotificationService } from 'src/app/services/notification/notification.service';

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
