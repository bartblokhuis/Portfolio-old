import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Url } from 'src/app/data/url';
import { ProjectsService } from 'src/app/services/api/projects/projects.service';
import { DeleteUrlComponent } from '../delete-url/delete-url.component';
import { EditUrlComponent } from '../edit-url/edit-url.component';

@Component({
  selector: 'app-list-url',
  templateUrl: './list-url.component.html',
  styleUrls: ['./list-url.component.scss']
})
export class ListUrlComponent implements OnInit {

  @Input() urls: Url[] = [];
  @Input() projectId: number | null = null;

  constructor(private readonly modalService: NgbModal, private readonly projectsService: ProjectsService ) { }

  ngOnInit(): void {
  }

  addUrl(): void {

  }

  editUrl(url: Url): void {
    this.openModel(EditUrlComponent, url);
  }

  deleteUrl(url: Url): void {
    this.openModel(DeleteUrlComponent, url)
  }

  openModel(component: any, url: Url | null = null): void {
    const modalRef = this.modalService.open(component, { size: 'lg' });

    if(url) {
      const editUrl: Url = { friendlyName: url.friendlyName, fullUrl: url.fullUrl, id: url.id  };
      modalRef.componentInstance.url = editUrl
    }
    modalRef.componentInstance.modal = modalRef;
    modalRef.componentInstance.projectId = this.projectId;
    
    modalRef.result.then(() => {
      this.refreshUrls();
    });
  }

  refreshUrls() {
    if(!this.projectId) return;
    this.projectsService.getProjectUrlsByProjectId(this.projectId).subscribe((result) => {
      if(result.succeeded) this.urls = result.data;
    })
  }
}