import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProjectPictureComponent } from './add-project-picture.component';

describe('AddProjectPictureComponent', () => {
  let component: AddProjectPictureComponent;
  let fixture: ComponentFixture<AddProjectPictureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddProjectPictureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddProjectPictureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
