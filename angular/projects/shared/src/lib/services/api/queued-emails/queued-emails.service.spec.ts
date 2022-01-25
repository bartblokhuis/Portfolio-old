import { TestBed } from '@angular/core/testing';

import { QueuedEmailsService } from './queued-emails.service';

describe('QueuedEmailsService', () => {
  let service: QueuedEmailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QueuedEmailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
