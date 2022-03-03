import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguagesUpdateComponent } from './languages-update.component';

describe('LanguagesUpdateComponent', () => {
  let component: LanguagesUpdateComponent;
  let fixture: ComponentFixture<LanguagesUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LanguagesUpdateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LanguagesUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
