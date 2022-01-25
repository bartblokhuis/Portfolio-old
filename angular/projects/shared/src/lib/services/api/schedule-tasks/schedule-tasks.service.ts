import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Result } from '../../../data/common/Result';
import { CreateScheduleTask } from '../../../data/schedule-task/create-schedule-task';
import { ScheduleTask } from '../../../data/schedule-task/schedule-task';
import { UpdateScheduleTask } from '../../../data/schedule-task/update-schedule-task';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class ScheduleTasksService {

  constructor(private apiService: ApiService) { }

  getAll(): Observable<Result<ScheduleTask[]>> {
    return this.apiService.get<ScheduleTask[]>('ScheduleTask');
  }

  create(scheduleTask: CreateScheduleTask): Observable<Result<ScheduleTask>> {
    return this.apiService.post<ScheduleTask>("ScheduleTask", scheduleTask)
  }

  update(scheduleTask: UpdateScheduleTask) {
    return this.apiService.put<ScheduleTask>("ScheduleTask", scheduleTask);
  }

  delete(id: number): Observable<Result> {
    return this.apiService.delete(`ScheduleTask?id=${id}`);
  }
}
