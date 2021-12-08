import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DatePrinterComponent } from './date-printer.component';

describe('DatePrinterComponent', () => {
  let component: DatePrinterComponent;
  let fixture: ComponentFixture<DatePrinterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DatePrinterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DatePrinterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
