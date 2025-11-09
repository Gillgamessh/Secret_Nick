import {
  AfterViewInit,
  Component,
  ElementRef,
  input,
  ViewChild,
} from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { InputSidebarText, InputPlaceholder, RegEx } from '../../../app.enum';
import { Input } from '../input/input';
import { InputType } from '../../../app.enum';
import type { InputLabel } from '../../../app.models';
import { generateId } from '../../../utils/generate-id';

@Component({
  selector: 'app-phone-input',
  imports: [Input, ReactiveFormsModule],
  templateUrl: './phone-input.html',
  styleUrl: './phone-input.scss',
})
export class PhoneInput implements AfterViewInit {
  @ViewChild(Input, { read: ElementRef })
  private inputRef!: ElementRef<HTMLInputElement>;
  readonly control = input.required<FormControl>();
  readonly label = input.required<InputLabel>();

  readonly isRequired = input<boolean>(false);
  readonly placeholder = input<InputPlaceholder>(InputPlaceholder.PhoneNumber);

  public readonly inputSidebarText = InputSidebarText.PhoneCodeUA;
  public readonly type = InputType.Tel;
  public readonly sidebarId = generateId();

  public onInput(event: Event): void {
    this.#restrictInput(event);
  }
  ngAfterViewInit(): void {
    const el = this.inputRef.nativeElement.querySelector('input');
    if (el) {
      el.addEventListener('input', this.#restrictInput.bind(this), true);
    }
  }
  #restrictInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    const pattern = new RegExp(RegEx.Digits, 'g');
    const digits = input.value.match(pattern);
    input.value = digits ? digits.join('') : '';

    if (input.value.length > 9) {
      input.value = input.value.slice(0, 9);
    }
  }
}
