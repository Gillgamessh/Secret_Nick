import { Component, input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

import {
  BaseLabel,
  FormSubtitle,
  FormTitle,
  InputPlaceholder,
  InputType,
  ItemPosition,
  PictureName,
  TextareaLabel,
  TextareaPlaceholder,
} from '../../../app.enum';
import { Textarea } from '../../../shared/components/textarea/textarea';
import { Input } from '../../../shared/components/input/input';
import { FormLayout } from '../../../shared/components/form-layout/form-layout';
import { CharCounter } from '../../../core/directives/char-counter';
import { PhoneInput } from '../../../shared/components/phone-input/phone-input';
import type { AddYourDetailsFormType } from '../../../app.models';
import { FieldHint } from '../../../shared/components/field-hint/field-hint';

@Component({
  selector: 'app-add-your-details-form',
  imports: [
    Textarea,
    Input,
    FormLayout,
    CharCounter,
    PhoneInput,
    ReactiveFormsModule,
    FieldHint,
  ],
  templateUrl: './add-your-details-form.html',
  styleUrl: './add-your-details-form.scss',
})
export class AddYourDetailsForm {
  readonly form = input.required<FormGroup<AddYourDetailsFormType>>();

  public readonly formTitle = FormTitle.AddDetails;
  public readonly formSubtitle = FormSubtitle.AddDetails;
  public readonly formPictureName = PictureName.Car;

  public readonly firstName = BaseLabel.FirstName;
  public readonly lastName = BaseLabel.LastName;
  public readonly phoneNumber = BaseLabel.PhoneNumber;
  public readonly email = BaseLabel.Email;
  public readonly deliveryAddress = TextareaLabel.DeliveryAddress;

  public readonly firstNamePlaceholder = InputPlaceholder.FirstName;
  public readonly lastNamePlaceholder = InputPlaceholder.LastName;
  public readonly phoneNumberPlaceholder = InputPlaceholder.PhoneNumber;
  public readonly emailPlaceholder = InputPlaceholder.Email;
  public readonly deliveryAddressPlaceholder =
    TextareaPlaceholder.DeliveryAddress;

  public readonly inputTypeEmail = InputType.Email;

  public readonly textareaMaxLength = 500;
  public readonly inputMaxLength = 40;
  public readonly inputCharCounterPositionY = ItemPosition.Center;

  public get firstNameControl(): FormControl {
    return this.form().get('firstName') as FormControl;
  }

  public get lastNameControl(): FormControl {
    return this.form().get('lastName') as FormControl;
  }

  public get phoneNumberControl(): FormControl {
    return this.form().get('phone') as FormControl;
  }

  public get emailControl(): FormControl {
    return this.form().get('email') as FormControl;
  }

  public get deliveryAddressControl(): FormControl {
    return this.form().get('deliveryInfo') as FormControl;
  }
}
