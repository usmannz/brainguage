import { Directive, ElementRef, Renderer2, Input, OnInit, HostListener } from '@angular/core';

@Directive({
  selector: '[appTruncateText]',
})
export class TruncateTextDirective implements OnInit {
  constructor(private el: ElementRef, private renderer: Renderer2) {}

  ngOnInit() {
    this.setStyles();
  }

  @HostListener('window:resize')
  onResize() {
    this.setStyles();
  }

  private setStyles() {
    const elementWidth = this.el.nativeElement.offsetWidth;
    const fontSize = parseFloat(window.getComputedStyle(this.el.nativeElement).fontSize);
    const padding = fontSize * 0.5; // set the padding to half of the font size
    const maxWidth = elementWidth - 2 * padding; // subtract the padding from the element width
    this.renderer.setStyle(this.el.nativeElement, 'max-width', `${maxWidth}px`);
    this.renderer.setStyle(this.el.nativeElement, 'overflow', 'hidden');
    this.renderer.setStyle(this.el.nativeElement, 'text-overflow', 'ellipsis');
    this.renderer.setStyle(this.el.nativeElement, 'white-space', 'nowrap');
    this.renderer.setStyle(this.el.nativeElement, 'padding-left', `${padding}px`);
    this.renderer.setStyle(this.el.nativeElement, 'padding-right', `${padding}px`);
  }
}