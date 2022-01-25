import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteScheduleTaskComponent } from './delete-schedule-task.component';

describe('DeleteScheduleTaskComponent', () => {
  let component: DeleteScheduleTaskComponent;
  let fixture: ComponentFixture<DeleteScheduleTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteScheduleTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteScheduleTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
