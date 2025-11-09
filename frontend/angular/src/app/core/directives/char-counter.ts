import {
  AfterViewInit,
  Directive,
  ElementRef,
  HostListener,
  inject,
  input,
  Renderer2,
} from '@angular/core';

import { ItemPosition } from '../../app.enum';
import { generateId } from '../../utils/generate-id';
import type { StyleMap } from '../../app.models';

@Directive({
  selector: '[appCharCounter]',
})
export class CharCounter implements AfterViewInit {
  readonly initCounterValue = input<number>(0);

  readonly positionX = input<ItemPosition>(ItemPosition.Right);
  readonly positionY = input<ItemPosition>(ItemPosition.Below);

  readonly #el = inject(ElementRef);
  readonly #renderer = inject(Renderer2);

  readonly #counterId: string = generateId();

  #maxLength!: string | null;
  #counterElement!: HTMLElement;
  #targetElement!: HTMLInputElement | HTMLTextAreaElement | null;

  ngAfterViewInit(): void {
    this.#targetElement = this.#findTarget();
    this.#maxLength = this.#targetElement?.getAttribute('maxlength') ?? null;

    if (this.#maxLength) {
      this.#renderCounter();
    }
  }

  @HostListener('input', ['$event.target']) onInput(
    target: EventTarget | null
  ): void {
    if (this.#maxLength) {
      const length = (target as HTMLInputElement | HTMLTextAreaElement).value
        .length;
      this.#updateCounter(length);
    }
  }

  #findTarget(): HTMLInputElement | HTMLTextAreaElement | null {
    const inputs = this.#el.nativeElement.querySelectorAll('input, textarea');

    return inputs.length
      ? (inputs[0] as HTMLInputElement | HTMLTextAreaElement)
      : null;
  }

  #getStyles(): StyleMap {
    const commonStyles: StyleMap = {
      position: 'absolute',
      'font-size': '14px',
      'line-height': '1.4',
      'font-weight': '400',
      color: '#909090',
      'pointer-events': 'none',
    };
    const positionStyles: Record<string, StyleMap> = {
      [`${ItemPosition.Right}-${ItemPosition.Center}`]: {
        top: '50%',
        right: '12px',
        transform: 'translateY(-50%)',
      },
      [`${ItemPosition.Right}-${ItemPosition.Below}`]: {
        bottom: '0',
        right: '0',
        transform: 'translateY(100%)',
      },
    };
    const positionKey = `${this.positionX()}-${this.positionY()}`;

    return { ...commonStyles, ...positionStyles[positionKey] };
  }

  #applyStyles(): void {
    const styles: StyleMap = this.#getStyles();

    Object.entries(styles).forEach(([styleName, value]) => {
      this.#renderer.setStyle(this.#counterElement, styleName, value);
    });

    this.#renderer.setStyle(this.#getParent(), 'position', 'relative');
  }

  #setAttributes(): void {
    const attributes = {
      id: this.#counterId,
      'aria-live': 'polite',
      'aria-atomic': 'true',
    };

    Object.entries(attributes).forEach(([key, value]) => {
      this.#renderer.setAttribute(this.#counterElement, key, value);
    });

    this.#renderer.setAttribute(
      this.#targetElement,
      'aria-describedby',
      this.#counterId
    );
  }

  #getParent(): HTMLElement {
    return this.#renderer.parentNode(this.#targetElement);
  }

  #updateCounter(length: number): void {
    this.#renderer.setProperty(
      this.#counterElement,
      'textContent',
      `${length} / ${this.#maxLength}`
    );
  }

  #renderCounter(): void {
    const initValue =
      this.#targetElement?.value?.length || this.initCounterValue();
    this.#counterElement = this.#renderer.createElement('span');
    this.#applyStyles();
    this.#setAttributes();
    this.#renderer.appendChild(this.#getParent(), this.#counterElement);
    this.#updateCounter(initValue);
  }
}
