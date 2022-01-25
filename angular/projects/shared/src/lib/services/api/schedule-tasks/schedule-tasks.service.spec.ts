import { TestBed } from '@angular/core/testing';

import { ScheduleTasksService } from './schedule-tasks.service';

describe('ScheduleTasksService', () => {
  let service: ScheduleTasksService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScheduleTasksService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
