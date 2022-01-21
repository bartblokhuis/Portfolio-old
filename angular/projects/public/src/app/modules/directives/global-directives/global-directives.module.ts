import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScrollToDirective } from '../../../directives/scroll-to.directive';
import { TooltipDirective } from '../../../directives/tooltip.directive';

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
