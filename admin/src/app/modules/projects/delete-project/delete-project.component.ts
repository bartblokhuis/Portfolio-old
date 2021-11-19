import { Component, Input } from '@angular/core';
import { Project } from '../../../data/project';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ProjectService } from 'src/app/services/projects/project.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-delete-project',
  templateUrl: './delete-project.component.html',
  styleUrls: ['./delete-project.component.scss']
})
export class DeleteProjectComponent {

  @Input() project: Project;
  @Input() modalRef: NgbModalRef;

  constructor(private projectSerivce: ProjectService, private toastr: ToastrService){ }

  close(){
    this.modalRef.close();
  }

  remove(id: number){
    this.projectSerivce.deleteProject(id).subscribe(() => {
      this.modalRef.close();
      this.toastr.success("Removed " + this.project.title);
    });
  }

}
