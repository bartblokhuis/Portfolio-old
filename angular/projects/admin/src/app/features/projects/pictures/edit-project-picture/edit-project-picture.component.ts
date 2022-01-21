import { Component, Input, OnInit } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ProjectsService } from 'projects/admin/src/app/services/api/projects/projects.service';
import { ProjectPicture } from 'projects/shared/src/lib/data/projects/project-picture';
import { UpdateProjectPicture } from 'projects/shared/src/lib/data/projects/update-project-picture';
import { GridPictureComponent } from '../../../pictures/grid-picture/grid-picture.component';
import { validateProjectPictureForm } from '../helpers/project-picture-helpers';

declare var $:any;
@Component({
  selector: 'app-edit-project-picture',
  templateUrl: './edit-project-picture.component.html',
  styleUrls: ['./edit-project-picture.component.scss']
})
export class EditProjectPictureComponent implements OnInit {

  @Input() modal: NgbModalRef | null = null;
  @Input() projectId: number | null = null;
  @Input() projectPicture: ProjectPicture | null = null;

  model: UpdateProjectPicture = { currentPictureId: 0, displayNumber: 0, newPictureId: 0, projectId: 0 }
  form: any;
  error: string | null = null;
  picturePath: string = '';
  
  constructor(private readonly modalService: NgbModal, private readonly projectsService: ProjectsService) { }

  ngOnInit(): void {
    if(this.projectId == null || this.projectPicture == null){
      this.modal?.close();
      return;
    }

    this.model.projectId = this.projectId;
    this.model.currentPictureId = this.projectPicture.pictureId;
    this.model.newPictureId = this.projectPicture.pictureId;
    this.model.displayNumber = this.projectPicture.displayNumber;
    this.picturePath = this.projectPicture.path;

    this.form = $("#editPictureForm");
    validateProjectPictureForm(this.form);
  }

  selectPicture(event: any) {
    const modalRef = this.modalService.open(GridPictureComponent, { size: 'lg' });
    modalRef.componentInstance.modal = modalRef;

    modalRef.result.then((result) => {
      if(!result) return;

      this.error = null;
      this.picturePath = result.path;
      this.model.newPictureId = result.id;
    })
  }

  save() {
    if(!this.form.valid()) return;
    this.error = null;

    if(this.model.newPictureId == 0) {
      this.error = "Please select a picture"
      return;
    }

    this.projectsService.updateProjectPicture(this.model).subscribe((result) => {
      if(result.succeeded) this.modal?.close();
      else{
        this.error = result.messages[0];
        return;
      }
    })
  }
}
