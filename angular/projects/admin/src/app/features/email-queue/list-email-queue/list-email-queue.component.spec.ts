import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListEmailQueueComponent } from './list-email-queue.component';

describe('ListEmailQueueComponent', () => {
  let component: ListEmailQueueComponent;
  let fixture: ComponentFixture<ListEmailQueueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListEmailQueueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListEmailQueueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
