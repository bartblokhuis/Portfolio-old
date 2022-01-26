import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { CreateScheduleTask } from 'projects/shared/src/lib/data/schedule-task/create-schedule-task';
import { ScheduleTasksService } from 'projects/shared/src/lib/services/api/schedule-tasks/schedule-tasks.service';
import { NotificationService } from '../../../services/notification/notification.service';
import { validateScheduleTaskForm } from '../helpers/schedule-task-helpers';

declare var $: any;

@Component({
  selector: 'app-add-schedule-task',
  templateUrl: './add-schedule-task.component.html',
  styleUrls: ['./add-schedule-task.component.scss']
})
export class AddScheduleTaskComponent implements OnInit {

  @Input() modal: NgbModalRef | null = null;

  model: CreateScheduleTask = { enabled: false, name: '', seconds: 1, stopOnError: false, type: '' };
  form: any;

  constructor(private readonly scheduleTasksService: ScheduleTasksService, private notificationService: NotificationService) { }

  ngOnInit(): void {

    this.form = $("#addScheduleTaskForm");
    validateScheduleTaskForm(this.form);
  }

  add() {
    if(!this.form.valid()) return;

    this.scheduleTasksService.create(this.model).subscribe((result) => {
      if(result.succeeded) {
        this.notificationService.success("Created the new schedule task");
        this.modal?.close();
      }
    })
  }

}
