import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProjectPicture } from 'src/app/data/projects/project-picture';
import { ProjectsService } from 'src/app/services/api/projects/projects.service';
import { environment } from 'src/environments/environment';
import { AddProjectPictureComponent } from '../add-project-picture/add-project-picture.component';
import { DeleteProjectPictureComponent } from '../delete-project-picture/delete-project-picture.component';
import { EditProjectPictureComponent } from '../edit-project-picture/edit-project-picture.component';

@Component({
  selector: 'app-list-project-pictures',
  templateUrl: './list-project-pictures.component.html',
  styleUrls: ['./list-project-pictures.component.scss']
})
export class ListProjectPicturesComponent implements OnInit {

  @Input() pictures: ProjectPicture[] | null = [];
  @Input() projectId: number = 0;

  baseUrl: string = environment.baseApiUrl;

  constructor(private readonly modalService: NgbModal, private readonly projectsService: ProjectsService) { }

  ngOnInit(): void {
    
  }

  addPicture() {
    this.openModel(AddProjectPictureComponent);
  }

  editPicture(picture: ProjectPicture) {
    this.openModel(EditProjectPictureComponent, picture);
  }

  deletePicture(picture: ProjectPicture) {
    this.openModel(DeleteProjectPictureComponent, picture);
  }

  openModel(component: any, projectPicture: ProjectPicture | null = null): void {
    const modalRef = this.modalService.open(component, { size: 'lg' });

    if(projectPicture) {
      const editPicture: ProjectPicture = { altAttribute: projectPicture.altAttribute, titleAttribute: projectPicture.titleAttribute, displayNumber: projectPicture.displayNumber, mimeType: projectPicture.mimeType, path: projectPicture.path, pictureId: projectPicture.pictureId  };
      modalRef.componentInstance.editPicture = editPicture
    }
    modalRef.componentInstance.modal = modalRef;
    modalRef.componentInstance.projectId = this.projectId;
    
    modalRef.result.then(() => {
      this.refreshPictures();
    });
  }

  refreshPictures() {
    this.projectsService.getProjectPicturesByProjectId(this.projectId).subscribe((result) => {
      if(result.succeeded) this.pictures = result.data;
    })
  }
}
