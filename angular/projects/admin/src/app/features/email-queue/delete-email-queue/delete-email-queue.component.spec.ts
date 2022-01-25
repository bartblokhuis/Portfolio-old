import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteEmailQueueComponent } from './delete-email-queue.component';

describe('DeleteEmailQueueComponent', () => {
  let component: DeleteEmailQueueComponent;
  let fixture: ComponentFixture<DeleteEmailQueueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteEmailQueueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteEmailQueueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
