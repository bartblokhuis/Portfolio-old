import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteProjectPictureComponent } from './delete-project-picture.component';

describe('DeleteProjectPictureComponent', () => {
  let component: DeleteProjectPictureComponent;
  let fixture: ComponentFixture<DeleteProjectPictureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteProjectPictureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteProjectPictureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
