import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListPictureComponent } from './list-picture.component';

describe('ListPictureComponent', () => {
  let component: ListPictureComponent;
  let fixture: ComponentFixture<ListPictureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListPictureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListPictureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
