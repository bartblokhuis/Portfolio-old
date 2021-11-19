import { TestBed } from '@angular/core/testing';

import { SettingserviceService } from './settingservice.service';

describe('SettingserviceService', () => {
  let service: SettingserviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SettingserviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
