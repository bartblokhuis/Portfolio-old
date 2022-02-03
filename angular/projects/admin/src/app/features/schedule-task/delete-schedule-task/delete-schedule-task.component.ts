import { Component, Input, OnInit } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { UpdateScheduleTask } from 'projects/shared/src/lib/data/schedule-task/update-schedule-task';
import { ScheduleTasksService } from 'projects/shared/src/lib/services/api/schedule-tasks/schedule-tasks.service';
import { NotificationService } from '../../../services/notification/notification.service';

@Component({
  selector: 'app-delete-schedule-task',
  templateUrl: './delete-schedule-task.component.html',
  styleUrls: ['./delete-schedule-task.component.scss']
})
export class DeleteScheduleTaskComponent implements OnInit {
  
  @Input() modal: NgbModalRef | null = null;
  @Input() model!: UpdateScheduleTask;
  apiError: string | null = null;

  constructor(private readonly scheduleTasksService: ScheduleTasksService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    if(!this.model) this.modal?.close;
  }

  remove() {
    this.apiError = null;
    if(!this.model) return;

    this.scheduleTasksService.delete(this.model.id).subscribe((result) => {
      if(!result.succeeded){
        this.apiError = result.messages[0];
        return;
      }
      this.notificationService.success("Removed the schedule task")
      this.modal?.close();
    });
  }

}
