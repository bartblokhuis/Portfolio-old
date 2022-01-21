import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ProjectsService } from 'projects/shared/src/lib/services/api/projects/projects.service';
import { NotificationService } from 'projects/admin/src/app/services/notification/notification.service';
import { ProjectPicture } from 'projects/shared/src/lib/data/projects/project-picture';

@Component({
  selector: 'app-delete-project-picture',
  templateUrl: './delete-project-picture.component.html',
  styleUrls: ['./delete-project-picture.component.scss']
})
export class DeleteProjectPictureComponent implements OnInit {

  @Input() modal: NgbModalRef | null = null;
  @Input() projectId: number | null = null;
  @Input() projectPicture: ProjectPicture | null = null;

  constructor(private readonly projectsService: ProjectsService, private readonly notificationService: NotificationService) { }

  ngOnInit(): void {
    if(!this.projectId || !this.projectPicture){
      this.modal?.close();
      return;
    }
  }

  remove() {
    if(!this.projectId || !this.projectPicture) return;

    this.projectsService.deleteProjectPicture(this.projectId, this.projectPicture.pictureId).subscribe((result) => {
      this.notificationService.success(`Removed the project picture`)
      this.modal?.close("removed");
    })
  }
}