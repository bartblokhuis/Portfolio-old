import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProjectPictureComponent } from './edit-project-picture.component';

describe('EditProjectPictureComponent', () => {
  let component: EditProjectPictureComponent;
  let fixture: ComponentFixture<EditProjectPictureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditProjectPictureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditProjectPictureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
