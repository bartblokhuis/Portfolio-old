import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletePictureComponent } from './delete-picture.component';

describe('DeletePictureComponent', () => {
  let component: DeletePictureComponent;
  let fixture: ComponentFixture<DeletePictureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeletePictureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeletePictureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
