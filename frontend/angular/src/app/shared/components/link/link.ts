import { Component, HostBinding, input } from '@angular/core';
import { AriaLabel } from '../../../app.enum';

@Component({
  selector: 'app-link',
  imports: [],
  templateUrl: './link.html',
  styleUrl: './link.scss',
})
export class Link {
  readonly text = input.required<string>();
  readonly link = input.required<string>();
  readonly ariaLabel = input<AriaLabel>();
  readonly isDisabled = input<boolean>(false);

  @HostBinding('class') get hostClass() {
    return `link ${this.isDisabled() ? 'link--disabled' : ''}`;
  }
}
