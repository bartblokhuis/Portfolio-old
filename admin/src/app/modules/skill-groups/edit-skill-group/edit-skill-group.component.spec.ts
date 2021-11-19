import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditSkillGroupComponent } from './edit-skill-group.component';

describe('EditSkillGroupComponent', () => {
  let component: EditSkillGroupComponent;
  let fixture: ComponentFixture<EditSkillGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditSkillGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditSkillGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
