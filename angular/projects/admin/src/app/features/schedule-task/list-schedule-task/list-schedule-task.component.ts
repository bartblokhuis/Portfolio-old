import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DataTableDirective } from 'angular-datatables';
import { BaseSearchModel } from 'projects/shared/src/lib/data/common/base-search-model';
import { ScheduleTask } from 'projects/shared/src/lib/data/schedule-task/schedule-task';
import { UpdateScheduleTask } from 'projects/shared/src/lib/data/schedule-task/update-schedule-task';
import { ScheduleTasksService } from 'projects/shared/src/lib/services/api/schedule-tasks/schedule-tasks.service';
import { Subject } from 'rxjs';
import { availablePageSizes, baseDataTableOptions } from '../../../helpers/datatable-helper';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { AddScheduleTaskComponent } from '../add-schedule-task/add-schedule-task.component';
import { DeleteScheduleTaskComponent } from '../delete-schedule-task/delete-schedule-task.component';
import { EditScheduleTaskComponent } from '../edit-schedule-task/edit-schedule-task.component';

@Component({
  selector: 'app-list-schedule-task',
  templateUrl: './list-schedule-task.component.html',
  styleUrls: ['./list-schedule-task.component.scss']
})
export class ListScheduleTaskComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;
  
  scheduleTasks: ScheduleTask[] = [];
  dtOptions: object = {};
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private readonly scheduleTasksService: ScheduleTasksService, private modalService: NgbModal, private readonly breadcrumbsService: BreadcrumbsService) { }

  ngOnInit(): void {

    const ajaxOptions = {  
      ajax: (dataTablesParameters: BaseSearchModel, callback: any) => {  
        const model : BaseSearchModel = { availablePageSizes: availablePageSizes.toString(), draw: dataTablesParameters.draw.toString(), length: dataTablesParameters.length, page: 0, pageSize: dataTablesParameters.length, start: dataTablesParameters.start }
        this.scheduleTasksService.list(model).subscribe((result) => {
          this.scheduleTasks = result.data.data;
          callback({
            recordsTotal: result.data.recordsTotal,
            recordsFiltered: result.data.recordsFiltered,
            draw: result.data.draw
          });
        });
      }
    }

    this.dtOptions = {...baseDataTableOptions, ...ajaxOptions}

    this.breadcrumbsService.setBreadcrumb([
      this.breadcrumbsService.homeCrumb,
      {
        name: 'System',
        url: undefined,
        active: true
      },
      {
        name: 'Schedule tasks',
        url: undefined,
        active: true
      }
    ]);
  }

  ngAfterViewInit(): void {
    this.dtTrigger.next(this.dtOptions);
  }

  edit(scheduleTask: ScheduleTask) {
    this.openModal(EditScheduleTaskComponent, scheduleTask);
  }

  delete(scheduleTask: ScheduleTask) {
    this.openModal(DeleteScheduleTaskComponent, scheduleTask);
  }

  add() {
    this.openModal(AddScheduleTaskComponent)
  }

  openModal(component: any, scheduleTask?: ScheduleTask) {
    const modalRef = this.modalService.open(component, { size: 'lg' });

    if(scheduleTask){
      var task: UpdateScheduleTask = { enabled: scheduleTask.enabled, id: scheduleTask.id, name: scheduleTask.name, seconds: scheduleTask.seconds, stopOnError: scheduleTask.stopOnError, type: scheduleTask.type };
      modalRef.componentInstance.model = task;
    }
    
    modalRef.componentInstance.modal = modalRef;
    
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
