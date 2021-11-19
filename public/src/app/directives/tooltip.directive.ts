import { Directive, ElementRef, Input } from '@angular/core';
import tippy from 'tippy.js';

@Directive({
  selector: '[tooltip]'
})
export class TooltipDirective {

  @Input('tippyOptions') public tippyOptions: Object | null = null;
  @Input('tippyContent') public tippyContent: string | null = null;

  constructor(private el: ElementRef) { 
    
    this.el = el;
  }

  public ngOnInit(){

    if(!this.tippyOptions && this.tippyContent){
      this.tippyOptions = { content: this.tippyContent }
    }
    
    tippy(this.el.nativeElement, this.tippyOptions || {content: 'ToolTip' });
  }
}
