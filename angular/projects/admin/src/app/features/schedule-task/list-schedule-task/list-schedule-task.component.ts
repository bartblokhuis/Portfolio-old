import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ScheduleTask } from 'projects/shared/src/lib/data/schedule-task/schedule-task';
import { UpdateScheduleTask } from 'projects/shared/src/lib/data/schedule-task/update-schedule-task';
import { ScheduleTasksService } from 'projects/shared/src/lib/services/api/schedule-tasks/schedule-tasks.service';
import { BreadcrumbsService } from '../../../services/breadcrumbs/breadcrumbs.service';
import { AddScheduleTaskComponent } from '../add-schedule-task/add-schedule-task.component';
import { DeleteScheduleTaskComponent } from '../delete-schedule-task/delete-schedule-task.component';
import { EditScheduleTaskComponent } from '../edit-schedule-task/edit-schedule-task.component';

@Component({
  selector: 'app-list-schedule-task',
  templateUrl: './list-schedule-task.component.html',
  styleUrls: ['./list-schedule-task.component.scss']
})
export class ListScheduleTaskComponent implements OnInit {

  scheduleTasks: ScheduleTask[] = [];

  constructor(private readonly scheduleTasksService: ScheduleTasksService, private modalService: NgbModal, private readonly breadcrumbsService: BreadcrumbsService) { }

  ngOnInit(): void {
    this.loadScheduleTasks();
  }

  loadScheduleTasks() {
    this.scheduleTasksService.getAll().subscribe((result) => {
      if(result.succeeded) this.scheduleTasks = result.data;
    })

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
      this.loadScheduleTasks();
    });
  }

}
