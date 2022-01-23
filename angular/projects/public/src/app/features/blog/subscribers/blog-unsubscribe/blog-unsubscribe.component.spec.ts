import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogUnsubscribeComponent } from './blog-unsubscribe.component';

describe('BlogUnsubscribeComponent', () => {
  let component: BlogUnsubscribeComponent;
  let fixture: ComponentFixture<BlogUnsubscribeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BlogUnsubscribeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BlogUnsubscribeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
