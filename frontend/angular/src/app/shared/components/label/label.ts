import { Component, input } from '@angular/core';

import type { InputLabel } from '../../../app.models';

@Component({
  selector: 'app-label',
  templateUrl: './label.html',
  styleUrl: './label.scss',
})
export class Label {
  readonly label = input.required<InputLabel>();

  readonly isLabelRadio = input<boolean>(false);
  readonly isRequiredField = input<boolean>(false);
}
