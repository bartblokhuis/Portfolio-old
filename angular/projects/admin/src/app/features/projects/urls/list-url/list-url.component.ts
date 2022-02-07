import { AfterViewInit, Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProjectsService } from 'projects/shared/src/lib/services/api/projects/projects.service';
import { Url } from 'projects/shared/src/lib/data/url';
import { AddUrlComponent } from '../add-url/add-url.component';
import { DeleteUrlComponent } from '../delete-url/delete-url.component';
import { EditUrlComponent } from '../edit-url/edit-url.component';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { Project } from 'projects/shared/src/lib/data/projects/project';
import { ProjectUrlSearch } from 'projects/shared/src/lib/data/projects/project-url-search';
import { availablePageSizes, baseDataTableOptions } from 'projects/admin/src/app/helpers/datatable-helper';

@Component({
  selector: 'app-list-url',
  templateUrl: './list-url.component.html',
  styleUrls: ['./list-url.component.scss']
})
export class ListUrlComponent implements OnInit, AfterViewInit, OnDestroy  {
  
  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;

  @Input() project: Subject<Project> = new Subject();
  urls: Url[] = [];
  projectId: number | null = null;
  dtOptions: object = {};
  dtTrigger: Subject<any> = new Subject<any>();


  constructor(private readonly modalService: NgbModal, private readonly projectsService: ProjectsService ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.project.subscribe((project) => {
      this.projectId = project.id;
      const ajaxOptions = {  
        ajax: (dataTablesParameters: ProjectUrlSearch, callback: any) => {  
          const model : ProjectUrlSearch = { availablePageSizes: availablePageSizes.toString(), draw: dataTablesParameters.draw.toString(), length: dataTablesParameters.length, page: 0, pageSize: dataTablesParameters.length, start: dataTablesParameters.start, projectId: project.id }
          this.projectsService.projectUrlList(model).subscribe((result) => {
            this.urls = result.data.data;
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
  addUrl(): void {
    this.openModel(AddUrlComponent);
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