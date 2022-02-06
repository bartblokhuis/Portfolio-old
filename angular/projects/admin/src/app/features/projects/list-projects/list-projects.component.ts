import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DataTableDirective } from 'angular-datatables';
import { BaseSearchModel } from 'projects/shared/src/lib/data/common/base-search-model';
import { Result } from 'projects/shared/src/lib/data/common/Result';
import { Project } from 'projects/shared/src/lib/data/projects/project';
import { ProjectsService } from 'projects/shared/src/lib/services/api/projects/projects.service';
import { Subject } from 'rxjs';
import { availablePageSizes, baseDataTableOptions } from '../../../helpers/datatable-helper';
import { AddProjectComponent } from '../add-project/add-project.component';
import { DeleteProjectComponent } from '../delete-project/delete-project.component';
import { EditProjectComponent } from '../edit-project/edit-project.component';

@Component({
  selector: 'app-list-projects',
  templateUrl: './list-projects.component.html',
  styleUrls: ['./list-projects.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ListProjectsComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;
  
  projects: Project[] = [];
  dtOptions: object = {};
  dtTrigger: Subject<any> = new Subject<any>();


  constructor(private projectsService: ProjectsService, private modalService: NgbModal) { }

  ngOnInit(): void {
    const ajaxOptions = {  
      ajax: (dataTablesParameters: BaseSearchModel, callback: any) => {  
        const model : BaseSearchModel = { availablePageSizes: availablePageSizes.toString(), draw: dataTablesParameters.draw.toString(), length: dataTablesParameters.length, page: 0, pageSize: dataTablesParameters.length, start: dataTablesParameters.start }
        this.projectsService.list(model).subscribe((result) => {
          this.projects = result.data.data;
          callback({
            recordsTotal: result.data.recordsTotal,
            recordsFiltered: result.data.recordsFiltered,
            draw: result.data.draw
          });
        });
      }
    }

    this.dtOptions = {...baseDataTableOptions, ...ajaxOptions}
  }

  ngAfterViewInit(): void {
    this.dtTrigger.next(this.dtOptions);
  }

  addProject() {
    this.openModal(AddProjectComponent)
  }

  editProject(project: Project) {
    this.openModal(EditProjectComponent, project);
  }

  deleteProject(project: Project) {
    this.openModal(DeleteProjectComponent, project);
  }

  openModal(component: any, project: Project | undefined = undefined){

    const modalRef = this.modalService.open(component, { size: 'lg' });

    if(project) {
      const editProject: Project = { description: project.description, displayNumber: project.displayNumber, id: project.id, isPublished: project.isPublished, title: project.title, urls: [], skills: project.skills, pictures: project.pictures  };
      modalRef.componentInstance.project = editProject
    }
    modalRef.componentInstance.modalRef = modalRef;
    
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
