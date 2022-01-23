import { TestBed } from '@angular/core/testing';

import { AboutMeService } from './about-me.service';

describe('AboutMeService', () => {
  let service: AboutMeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AboutMeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
