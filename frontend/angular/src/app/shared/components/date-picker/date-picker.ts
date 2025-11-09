import { Component, computed, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { differenceInCalendarDays } from 'date-fns';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';

import { Label } from '../label/label';
import { InputLabel } from '../../../app.models';
import { InputPlaceholder } from '../../../app.enum';

@Component({
  selector: 'app-date-picker',
  imports: [ReactiveFormsModule, NzDatePickerModule, Label],
  templateUrl: './date-picker.html',
  styleUrl: './date-picker.scss',
})
export class DatePicker {
  control = input.required<FormControl<Date | null>>();
  label = input.required<InputLabel>();
  placeholder = input<InputPlaceholder>(InputPlaceholder.Date);
  enableFromDaysOffset = input<number>(0);
  isRequiredField = input<boolean>(false);
  #today = new Date();

  disabledDate = computed(() => {
    return (current: Date): boolean => {
      const daysDiff = differenceInCalendarDays(current, this.#today);
      return daysDiff < this.enableFromDaysOffset();
    };
  });

  onOpenChange(isOpen: boolean): void {
    if (!isOpen) {
      this.control().markAsTouched();
      this.control().updateValueAndValidity({
        onlySelf: true,
        emitEvent: true,
      });
    }
  }
}
