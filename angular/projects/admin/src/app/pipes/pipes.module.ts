import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DatePrinterPipe } from './date-printer.pipe';



@NgModule({
  declarations: [DatePrinterPipe],
  imports: [
    CommonModule
  ],
  exports: [
    DatePrinterPipe
  ]
})
export class PipesModule { }
