import {
  AfterViewInit,
  Directive,
  DOCUMENT,
  ElementRef,
  HostListener,
  inject,
  OnDestroy,
} from '@angular/core';

const FOCUSABLE_SELECTORS = [
  'a[href]',
  'area[href]',
  'input:not([disabled])',
  'select:not([disabled])',
  'textarea:not([disabled])',
  'button:not([disabled])',
  '[tabindex]:not([tabindex="-1"])',
];

@Directive({
  selector: '[appFocusTrap]',
})
export class FocusTrap implements AfterViewInit, OnDestroy {
  readonly #host = inject<ElementRef<HTMLElement>>(ElementRef);
  readonly #document = inject(DOCUMENT);

  #focusableEls: HTMLElement[] = [];
  #firstEl?: HTMLElement;
  #lastEl?: HTMLElement;
  #previouslyFocusedElement?: HTMLElement;

  ngAfterViewInit(): void {
    this.#previouslyFocusedElement = this.#document
      .activeElement as HTMLElement;

    this.#updateFocusableElements();

    (this.#firstEl ? this.#firstEl : this.#host.nativeElement)?.focus();
  }

  ngOnDestroy(): void {
    if (this.#previouslyFocusedElement) {
      this.#previouslyFocusedElement.focus();
    }

    this.#focusableEls = [];
    this.#firstEl = undefined;
    this.#lastEl = undefined;
  }

  @HostListener('keydown', ['$event'])
  trapFocus(event: KeyboardEvent): void {
    const { shiftKey } = event;
    const activeEl = this.#document.activeElement;
    const isAtEdge =
      (!shiftKey && activeEl === this.#lastEl)
      || (shiftKey && activeEl === this.#firstEl);

    if (event.key !== 'Tab' || this.#focusableEls.length === 0) {
      return;
    }

    if (isAtEdge) {
      event.preventDefault();
      (shiftKey ? this.#lastEl : this.#firstEl)?.focus();
    }
  }

  #updateFocusableElements(): void {
    this.#focusableEls = Array.from(
      this.#host.nativeElement.querySelectorAll<HTMLElement>(
        FOCUSABLE_SELECTORS.join(', ')
      )
    );

    this.#firstEl = this.#focusableEls[0];
    this.#lastEl = this.#focusableEls[this.#focusableEls.length - 1];
  }
}
