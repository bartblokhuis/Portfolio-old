import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguagesDeleteComponent } from './languages-delete.component';

describe('LanguagesDeleteComponent', () => {
  let component: LanguagesDeleteComponent;
  let fixture: ComponentFixture<LanguagesDeleteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LanguagesDeleteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LanguagesDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
