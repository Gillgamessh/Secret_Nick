import { Component, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { Label } from '../label/label';
import {
  RadioButtonLabel,
  RadioButtonValue,
  RadioGroupName,
} from '../../../app.enum';

@Component({
  selector: 'app-radio-button',
  imports: [Label, ReactiveFormsModule],
  templateUrl: './radio-button.html',
  styleUrl: './radio-button.scss',
})
export class RadioButton {
  control = input.required<FormControl>();
  value = input.required<RadioButtonValue>();
  label = input.required<RadioButtonLabel>();
  name = input.required<RadioGroupName>();
}
