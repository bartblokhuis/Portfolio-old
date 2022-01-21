import { Directive, ElementRef, HostListener, Input } from '@angular/core';
import tippy from 'tippy.js';

@Directive({
  selector: '[tooltip]'
})
export class TooltipDirective {

  @Input('tippyOptions') public tippyOptions: Object | null = null;
  @Input('tippyContent') public tippyContent: string | null = null;

  //Use host listener instead of ng on init in angular ssr
  @HostListener("window:load", []) onWindowLoad() {
    if(!this.tippyOptions && this.tippyContent){
      this.tippyOptions = { content: this.tippyContent }
    }
    
    tippy(this.el.nativeElement, this.tippyOptions || {content: 'ToolTip' });
  }

  constructor(private el: ElementRef) { 
    this.el = el;
  }

  public ngOnInit(){
  }
}
