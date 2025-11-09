import { Component, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import {
  InputType,
  InputPlaceholder,
  InputSidebarText,
  RegEx,
} from '../../../app.enum';
import { Input } from '../input/input';
import type { InputLabel } from '../../../app.models';
import { generateId } from '../../../utils/generate-id';

@Component({
  selector: 'app-budget-input',
  imports: [Input, ReactiveFormsModule],
  templateUrl: './budget-input.html',
  styleUrl: './budget-input.scss',
})
export class BudgetInput {
  readonly control = input.required<FormControl>();
  readonly label = input.required<InputLabel>();

  readonly isRequired = input<boolean>(false);
  readonly placeholder = input<InputPlaceholder>(InputPlaceholder.Budget);

  public readonly inputSidebarText = InputSidebarText.CurrencyUAH;
  public readonly type = InputType.Number;
  public readonly sidebarId = generateId();

  public onInput(event: Event): void {
    this.#restrictInput(event);
  }

  #restrictInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    const pattern = new RegExp(RegEx.Digits, 'g');
    const digits = input.value.match(pattern);
    const clearValue = Number(digits ? digits.join('') : '');
    input.value = clearValue.toString();

    this.control().setValue(clearValue, { emitEvent: false });
  }
}
