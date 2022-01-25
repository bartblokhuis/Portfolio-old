import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListScheduleTaskComponent } from './list-schedule-task.component';

describe('ListScheduleTaskComponent', () => {
  let component: ListScheduleTaskComponent;
  let fixture: ComponentFixture<ListScheduleTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListScheduleTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListScheduleTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
