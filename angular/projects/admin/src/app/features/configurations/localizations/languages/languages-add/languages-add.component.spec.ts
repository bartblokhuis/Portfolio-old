import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguagesAddComponent } from './languages-add.component';

describe('LanguagesAddComponent', () => {
  let component: LanguagesAddComponent;
  let fixture: ComponentFixture<LanguagesAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LanguagesAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LanguagesAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
