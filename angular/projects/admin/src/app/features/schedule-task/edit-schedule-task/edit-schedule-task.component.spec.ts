import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditScheduleTaskComponent } from './edit-schedule-task.component';

describe('EditScheduleTaskComponent', () => {
  let component: EditScheduleTaskComponent;
  let fixture: ComponentFixture<EditScheduleTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditScheduleTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditScheduleTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
