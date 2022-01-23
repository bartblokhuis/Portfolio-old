import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteBlogSubscriberComponent } from './delete-blog-subscriber.component';

describe('DeleteBlogSubscriberComponent', () => {
  let component: DeleteBlogSubscriberComponent;
  let fixture: ComponentFixture<DeleteBlogSubscriberComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteBlogSubscriberComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteBlogSubscriberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
