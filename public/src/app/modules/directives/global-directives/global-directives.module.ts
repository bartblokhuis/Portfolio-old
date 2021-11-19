import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TooltipDirective } from 'src/app/directives/tooltip.directive';
import { ScrollToDirective } from 'src/app/directives/scroll-to.directive';



@NgModule({
  declarations: [
    TooltipDirective,
    ScrollToDirective
  ],
  exports: [
    TooltipDirective,
    ScrollToDirective
  ],
  imports: [
    CommonModule
  ]
})
export class GlobalDirectivesModule { }
