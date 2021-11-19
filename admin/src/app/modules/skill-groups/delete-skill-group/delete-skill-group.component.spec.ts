import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteSkillGroupComponent } from './delete-skill-group.component';

describe('DeleteSkillGroupComponent', () => {
  let component: DeleteSkillGroupComponent;
  let fixture: ComponentFixture<DeleteSkillGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeleteSkillGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteSkillGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
