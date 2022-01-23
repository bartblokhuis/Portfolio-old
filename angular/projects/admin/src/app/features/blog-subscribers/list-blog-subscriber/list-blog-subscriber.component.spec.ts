import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListBlogSubscriberComponent } from './list-blog-subscriber.component';

describe('ListBlogSubscriberComponent', () => {
  let component: ListBlogSubscriberComponent;
  let fixture: ComponentFixture<ListBlogSubscriberComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListBlogSubscriberComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListBlogSubscriberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
