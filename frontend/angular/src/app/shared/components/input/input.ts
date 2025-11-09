import { Component, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { InputType } from '../../../app.enum';
import { Label } from '../label/label';
import { InputPlaceholder } from '../../../app.enum';
import type { InputLabel } from '../../../app.models';

@Component({
  selector: 'app-input',
  imports: [ReactiveFormsModule, Label],
  templateUrl: './input.html',
  styleUrl: './input.scss',
})
export class Input {
  readonly control = input.required<FormControl>();
  readonly label = input.required<InputLabel>();

  readonly type = input<InputType>(InputType.Text);
  readonly placeholder = input<InputPlaceholder | ''>('');
  readonly isRequiredField = input<boolean>(false);
  readonly maxLength = input<number | null>(null);
  readonly ariaDescribedBy = input<string>();

  get ariaInvalid(): 'true' | 'false' {
    return (this.control().touched || this.control().dirty)
      && this.control().invalid
      ? 'true'
      : 'false';
  }
}
