import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogSettingsComponent } from './blog-settings.component';

describe('BlogSettingsComponent', () => {
  let component: BlogSettingsComponent;
  let fixture: ComponentFixture<BlogSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BlogSettingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BlogSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
