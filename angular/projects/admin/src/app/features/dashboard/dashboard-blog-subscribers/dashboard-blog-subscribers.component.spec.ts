import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardBlogSubscribersComponent } from './dashboard-blog-subscribers.component';

describe('DashboardBlogSubscribersComponent', () => {
  let component: DashboardBlogSubscribersComponent;
  let fixture: ComponentFixture<DashboardBlogSubscribersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DashboardBlogSubscribersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardBlogSubscribersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
