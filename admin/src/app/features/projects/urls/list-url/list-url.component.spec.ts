import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListUrlComponent } from './list-url.component';

describe('ListUrlComponent', () => {
  let component: ListUrlComponent;
  let fixture: ComponentFixture<ListUrlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListUrlComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListUrlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
