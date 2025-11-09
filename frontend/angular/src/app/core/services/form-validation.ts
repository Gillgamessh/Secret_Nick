import { Injectable } from '@angular/core';
import { AbstractControl, FormControl, ValidationErrors } from '@angular/forms';

import { ErrorMessage, RegEx } from '../../app.enum';
import { CustomError } from '../../app.models';
import { MAX_BUDGET } from '../../app.constants';

@Injectable({
  providedIn: 'root',
})
export class FormValidation {
  public getErrorMessage(control: FormControl): ErrorMessage {
    const errors: ValidationErrors = control.errors || {};

    return (
      ErrorMessage[Object.keys(errors)[0] as keyof typeof ErrorMessage]
      || ErrorMessage.DefaultMessage
    );
  }

  static phone(control: AbstractControl): CustomError | null {
    const value = control.value as string;
    const phoneRegExp = new RegExp(RegEx.Phone);
    if (!value) return null;

    if (!phoneRegExp.test(value)) {
      return { invalidPhone: true };
    }

    return null;
  }

  static safeUrl(control: AbstractControl): CustomError | null {
    const value = control.value as string;
    if (!value) return null;

    const urlRegExp = new RegExp(RegEx.SafeUrl, 'i');
    return urlRegExp.test(value) ? null : { unsafeUrl: true };
  }

  static budgetInRange(control: AbstractControl): CustomError | null {
    const value = control.value as string;
    if (+value > MAX_BUDGET) {
      return { budgetOutOfRange: true };
    }
    return null;
  }
}
