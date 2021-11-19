import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListSkillGroupComponent } from './list-skill-group.component';

describe('ListSkillGroupComponent', () => {
  let component: ListSkillGroupComponent;
  let fixture: ComponentFixture<ListSkillGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListSkillGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListSkillGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
