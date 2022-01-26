import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { UpdateScheduleTask } from 'projects/shared/src/lib/data/schedule-task/update-schedule-task';
import { ScheduleTasksService } from 'projects/shared/src/lib/services/api/schedule-tasks/schedule-tasks.service';
import { NotificationService } from '../../../services/notification/notification.service';
import { validateScheduleTaskForm } from '../helpers/schedule-task-helpers';

declare var $: any;

@Component({
  selector: 'app-edit-schedule-task',
  templateUrl: './edit-schedule-task.component.html',
  styleUrls: ['./edit-schedule-task.component.scss']
})
export class EditScheduleTaskComponent implements OnInit {

  @Input() modal: NgbModalRef | null = null;
  @Input() model: UpdateScheduleTask = { enabled: false, name: '', seconds: 0, stopOnError: false, type: '', id: 0 };
  form: any;

  constructor(private readonly scheduleTasksService: ScheduleTasksService, private notificationService: NotificationService) { }

  ngOnInit(): void {

    this.form = $("#editScheduleTaskForm");
    validateScheduleTaskForm(this.form);
    
  }

  save(): void {
    if(!this.form.valid()) return;

    this.scheduleTasksService.update(this.model).subscribe((result) => {
      if(result.succeeded) {
        this.notificationService.success("Updated the schedule task");
        this.modal?.close();
      }
    })
  }

}
