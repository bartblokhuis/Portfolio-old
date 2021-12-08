import { TestBed } from '@angular/core/testing';

import { ContentTitleService } from './content-title.service';

describe('ContentTitleService', () => {
  let service: ContentTitleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ContentTitleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
