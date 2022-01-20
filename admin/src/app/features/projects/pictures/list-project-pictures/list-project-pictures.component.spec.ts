import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListProjectPicturesComponent } from './list-project-pictures.component';

describe('ListProjectPicturesComponent', () => {
  let component: ListProjectPicturesComponent;
  let fixture: ComponentFixture<ListProjectPicturesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListProjectPicturesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListProjectPicturesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
