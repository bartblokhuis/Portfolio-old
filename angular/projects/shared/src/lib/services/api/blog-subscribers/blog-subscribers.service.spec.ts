import { TestBed } from '@angular/core/testing';

import { BlogSubscribersService } from './blog-subscribers.service';

describe('BlogSubscribersService', () => {
  let service: BlogSubscribersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlogSubscribersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
