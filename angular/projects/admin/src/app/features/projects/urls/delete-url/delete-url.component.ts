import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ProjectsService } from 'projects/admin/src/app/services/api/projects/projects.service';
import { NotificationService } from 'projects/admin/src/app/services/notification/notification.service';
import { Url } from 'projects/shared/src/lib/data/url';

@Component({
  selector: 'app-delete-url',
  templateUrl: './delete-url.component.html',
  styleUrls: ['./delete-url.component.scss']
})
export class DeleteUrlComponent implements OnInit {

  @Input() modal: NgbModalRef | null = null;
  @Input() url: Url | null = null;
  @Input() projectId: number | null = null;

  constructor(private readonly projectsService: ProjectsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    if(!this.url || !this.projectId){
      this.modal?.close();
    }
  }

  remove() {
    if(!this.url || !this.projectId){
      this.modal?.close();
      return;
    }

    this.projectsService.deleteProjectUrl(this.projectId, this.url.id).subscribe((result) => {
      this.notificationService.success("Removed the project url")
      this.modal?.close();
    });
  }

}
