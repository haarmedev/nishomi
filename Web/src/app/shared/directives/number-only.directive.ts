import { Directive, HostListener, Input } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
  selector: '[appNumberOnly]',
})
export class NumberOnlyDirective {
  private el: NgControl;
  @Input() maxlen = 50;

  constructor(private ngControl: NgControl) {
    this.el = ngControl;
  }

  @HostListener('input', ['$event.target.value'])
  onInput(value: string): void {
    this.el.control.patchValue(value.replace(/[^0-9]/g, '').substring(0, this.maxlen));
  }
}
