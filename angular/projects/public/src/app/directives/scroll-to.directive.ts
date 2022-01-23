import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[ScrollTo]'
})
export class ScrollToDirective {

  @Input('scrollToId') public scrollToId: string | null = null;

  constructor(private el: ElementRef) { }

  @HostListener('click') onMouseLeave() {
    if(this.scrollToId){
      const elToScrollTo = document.getElementById(this.scrollToId);
      elToScrollTo?.scrollIntoView({ behavior: 'smooth' });
    }
  }
}
