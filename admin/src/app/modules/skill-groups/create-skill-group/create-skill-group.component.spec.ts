import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSkillGroupComponent } from './create-skill-group.component';

describe('CreateSkillGroupComponent', () => {
  let component: CreateSkillGroupComponent;
  let fixture: ComponentFixture<CreateSkillGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateSkillGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateSkillGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
