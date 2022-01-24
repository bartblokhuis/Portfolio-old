import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteAllEmailQueueComponent } from './delete-all-email-queue.component';

describe('DeleteAllEmailQueueComponent', () => {
  let component: DeleteAllEmailQueueComponent;
  let fixture: ComponentFixture<DeleteAllEmailQueueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteAllEmailQueueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteAllEmailQueueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
