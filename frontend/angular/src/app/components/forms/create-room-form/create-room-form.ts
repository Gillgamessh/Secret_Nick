import { Component, input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

import { FormLayout } from '../../../shared/components/form-layout/form-layout';
import { Textarea } from '../../../shared/components/textarea/textarea';
import { DatePicker } from '../../../shared/components/date-picker/date-picker';
import { Input } from '../../../shared/components/input/input';
import {
  BaseLabel,
  CaptionMessage,
  FormSubtitle,
  FormTitle,
  InputPlaceholder,
  ItemPosition,
  PictureName,
  TextareaLabel,
  TextareaPlaceholder,
} from '../../../app.enum';
import { BudgetInput } from '../../../shared/components/budget-input/budget-input';
import { CharCounter } from '../../../core/directives/char-counter';
import { FieldHint } from '../../../shared/components/field-hint/field-hint';
import type { CreateRoomFormType } from '../../../app.models';

@Component({
  selector: 'app-create-room-form',
  imports: [
    FormLayout,
    Input,
    Textarea,
    DatePicker,
    BudgetInput,
    CharCounter,
    ReactiveFormsModule,
    FieldHint,
  ],
  templateUrl: './create-room-form.html',
  styleUrl: './create-room-form.scss',
})
export class CreateRoomForm {
  readonly form = input.required<FormGroup<CreateRoomFormType>>();

  public readonly formTitle = FormTitle.CreateRoom;
  public readonly formSubtitle = FormSubtitle.CreateRoom;
  public readonly formPictureName = PictureName.Star;

  public readonly roomName = BaseLabel.RoomName;
  public readonly exchangeDate = BaseLabel.ExchangeDate;
  public readonly giftBudget = BaseLabel.Budget;
  public readonly roomDescription = TextareaLabel.RoomDescription;

  public readonly roomNamePlaceholder = InputPlaceholder.EnterName;
  public readonly exchangeDatePlaceholder = InputPlaceholder.Date;
  public readonly giftBudgetPlaceholder = InputPlaceholder.Budget;
  public readonly roomDescriptionPlaceholder = TextareaPlaceholder.EnterMessage;

  public readonly giftBudgetCaptionMessage = CaptionMessage.BudgetExplanation;

  public readonly textareaMaxLength = 200;
  public readonly inputMaxLength = 40;
  public readonly inputCharCounterPositionY = ItemPosition.Center;

  public get roomNameControl(): FormControl {
    return this.form().get('name') as FormControl;
  }

  public get roomDescriptionControl(): FormControl {
    return this.form().get('description') as FormControl;
  }

  public get exchangeDateControl(): FormControl {
    return this.form().get('giftExchangeDate') as FormControl;
  }

  public get giftBudgetControl(): FormControl {
    return this.form().get('giftMaximumBudget') as FormControl;
  }
}
