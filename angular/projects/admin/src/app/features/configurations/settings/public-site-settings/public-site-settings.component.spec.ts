import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicSiteSettingsComponent } from './public-site-settings.component';

describe('PublicSiteSettingsComponent', () => {
  let component: PublicSiteSettingsComponent;
  let fixture: ComponentFixture<PublicSiteSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublicSiteSettingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicSiteSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
