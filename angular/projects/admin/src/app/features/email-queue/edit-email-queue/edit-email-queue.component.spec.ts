import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEmailQueueComponent } from './edit-email-queue.component';

describe('EditEmailQueueComponent', () => {
  let component: EditEmailQueueComponent;
  let fixture: ComponentFixture<EditEmailQueueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditEmailQueueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditEmailQueueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
