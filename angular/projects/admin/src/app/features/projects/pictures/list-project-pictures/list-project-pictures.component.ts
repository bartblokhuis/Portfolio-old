import { AfterViewInit, Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProjectsService } from 'projects/shared/src/lib/services/api/projects/projects.service';
import { environment } from 'projects/admin/src/environments/environment';
import { ProjectPicture } from 'projects/shared/src/lib/data/projects/project-picture';
import { AddProjectPictureComponent } from '../add-project-picture/add-project-picture.component';
import { DeleteProjectPictureComponent } from '../delete-project-picture/delete-project-picture.component';
import { EditProjectPictureComponent } from '../edit-project-picture/edit-project-picture.component';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { availablePageSizes, baseDataTableOptions } from 'projects/admin/src/app/helpers/datatable-helper';
import { BaseSearchModel } from 'projects/shared/src/lib/data/common/base-search-model';
import { ProjectPictureSearch } from 'projects/shared/src/lib/data/projects/project-picture-search';
import { Project } from 'projects/shared/src/lib/data/projects/project';

@Component({
  selector: 'app-list-project-pictures',
  templateUrl: './list-project-pictures.component.html',
  styleUrls: ['./list-project-pictures.component.scss']
})
export class ListProjectPicturesComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;
  
  @Input() project: Subject<Project> = new Subject();
  projectId: number | null = null;

  pictures: ProjectPicture[] | null = [];
  baseUrl: string = environment.baseApiUrl;
  dtOptions: object = {};
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private readonly modalService: NgbModal, private readonly projectsService: ProjectsService) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.project.subscribe((project) => {
      this.projectId = project.id;
      const ajaxOptions = {  
        ajax: (dataTablesParameters: ProjectPictureSearch, callback: any) => {  
          const model : ProjectPictureSearch = { availablePageSizes: availablePageSizes.toString(), draw: dataTablesParameters.draw.toString(), length: dataTablesParameters.length, page: 0, pageSize: dataTablesParameters.length, start: dataTablesParameters.start, projectId: project.id }
          this.projectsService.projectPictureList(model).subscribe((result) => {
            this.pictures = result.data.data;
            callback({
              recordsTotal: result.data.recordsTotal,
              recordsFiltered: result.data.recordsFiltered,
              draw: result.data.draw
            });
          });
        }
      }

      this.dtOptions = {...baseDataTableOptions, ...ajaxOptions}
      this.dtTrigger.next(this.dtOptions);
    });
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
        modalRef.componentInstance.projectPicture = editPicture
      }

      modalRef.componentInstance.modal = modalRef;
      modalRef.componentInstance.projectId = this.projectId;

      modalRef.result.then(() => {
        this.refresh();
      });
  }

  refresh(): void {
    this.dtElement?.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.destroy();

      this.dtTrigger.next(this.dtOptions);
    })
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }
}
