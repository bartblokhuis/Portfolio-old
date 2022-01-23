import { Component, Input, OnInit } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ProjectsService } from 'projects/shared/src/lib/services/api/projects/projects.service';
import { AddProjectPicture } from 'projects/shared/src/lib/data/projects/add-project-picture';
import { GridPictureComponent } from '../../../pictures/grid-picture/grid-picture.component';
import { validateProjectPictureForm } from '../helpers/project-picture-helpers';

declare var $: any;
@Component({
  selector: 'app-add-project-picture',
  templateUrl: './add-project-picture.component.html',
  styleUrls: ['./add-project-picture.component.scss']
})
export class AddProjectPictureComponent implements OnInit {

  @Input() modal: NgbModalRef | null = null;
  @Input() projectId: number | null = null;

  model: AddProjectPicture = { displayNumber: 0, pictureId: 0, projectId: 0 };
  picturePath: string = '';
  error: string | null = null;
  form: any;

  constructor(private readonly modalService: NgbModal, private readonly projectsService: ProjectsService) { }

  ngOnInit(): void {

    this.form = $("#addPictureForm");
    this.model.projectId = this.projectId ?? 0;
    validateProjectPictureForm(this.form);
  }

  addPicture(event: any) {
    const modalRef = this.modalService.open(GridPictureComponent, { size: 'lg' });
    modalRef.componentInstance.modal = modalRef;

    modalRef.result.then((result) => {
      if(!result) return;

      this.error = null;
      this.picturePath = result.path;
      this.model.pictureId = result.id;
    })
  }

  add() {
    if(!this.form.valid()) return;
    this.error = null;

    if(this.model.pictureId == 0) {
      this.error = "Please select a picture"
      return;
    }

    this.projectsService.createProjectPicture(this.model).subscribe((result) => {
      if(result.succeeded) this.modal?.close();
      else{
        this.error = result.messages[0];
        return;
      }
    })

    
  }

}
