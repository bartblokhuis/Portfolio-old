import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GridPictureComponent } from './grid-picture.component';

describe('GridPictureComponent', () => {
  let component: GridPictureComponent;
  let fixture: ComponentFixture<GridPictureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GridPictureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GridPictureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
