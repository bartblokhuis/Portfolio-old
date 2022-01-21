import { TestBed } from '@angular/core/testing';

import { SkillGroupsService } from './skill-groups.service';

describe('SkillGroupsService', () => {
  let service: SkillGroupsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SkillGroupsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
