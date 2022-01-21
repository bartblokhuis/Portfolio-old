import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListSkillGroupsComponent } from './list-skill-groups.component';

describe('ListSkillGroupsComponent', () => {
  let component: ListSkillGroupsComponent;
  let fixture: ComponentFixture<ListSkillGroupsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListSkillGroupsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListSkillGroupsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
