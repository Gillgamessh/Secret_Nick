import {
  Component,
  inject,
  input,
  OnDestroy,
  OnInit,
  output,
  signal,
} from '@angular/core';
import {
  FormArray,
  FormControl,
  FormControlStatus,
  FormGroup,
  NonNullableFormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';
import { Subscription } from 'rxjs';

import { FormLayout } from '../../../shared/components/form-layout/form-layout';
import { Textarea } from '../../../shared/components/textarea/textarea';
import { RadioButton } from '../../../shared/components/radio-button/radio-button';
import { GiftIdea } from '../../../shared/components/gift-idea/gift-idea';
import {
  ButtonText,
  FormSubtitle,
  FormTitle,
  IconName,
  ItemPosition,
  PictureName,
  RadioButtonLabel,
  RadioButtonValue,
  RadioGroupName,
  TextareaLabel,
  TextareaPlaceholder,
} from '../../../app.enum';
import { Button } from '../../../shared/components/button/button';
import { CharCounter } from '../../../core/directives/char-counter';
import type {
  BudgetControl,
  GiftIdeaFormType,
  SurpriseGiftFormType,
} from '../../../app.models';
import { FormValidation as CustomValidators } from '../../../core/services/form-validation';

@Component({
  selector: 'app-add-your-wishes-form',
  imports: [
    FormLayout,
    Textarea,
    RadioButton,
    GiftIdea,
    Button,
    CharCounter,
    ReactiveFormsModule,
  ],
  templateUrl: './add-your-wishes-form.html',
  styleUrl: './add-your-wishes-form.scss',
})
export class AddYourWishesForm implements OnInit, OnDestroy {
  readonly giftIdeaForm = input.required<FormGroup<GiftIdeaFormType>>();
  readonly surpriseGiftForm = input.required<FormGroup<SurpriseGiftFormType>>();
  readonly radioControl = input.required<FormControl<string>>();
  readonly budgetControl = input.required<FormControl<BudgetControl>>();

  readonly formCompleted = output<void>();

  readonly #formBuilder = inject(NonNullableFormBuilder);

  public readonly isFormValid = signal<boolean>(false);

  public readonly formTitle = FormTitle.AddWishes;
  public readonly formSubtitle = FormSubtitle.AddWishes;
  public readonly formPictureName = PictureName.BigPresents;
  public readonly radioName = RadioGroupName.Wishlist;
  public readonly wishButtonText = ButtonText.AddWish;
  public readonly wishButtonIconName = IconName.Add;
  public readonly giftIdeasRadioLabel = RadioButtonLabel.HaveGiftIdeas;
  public readonly giftIdeasRadioValue = RadioButtonValue.WishGift;
  public readonly surpriseGiftRadioLabel = RadioButtonLabel.WantSurpriseGift;
  public readonly surpriseGiftRadioValue = RadioButtonValue.SurpriseGift;
  public readonly surpriseGiftAreaLabel = TextareaLabel.AddInterests;
  public readonly surpriseGiftAreaPlaceholder =
    TextareaPlaceholder.AddInterests;
  public readonly inputCharCounterPositionY = ItemPosition.Center;
  public readonly textareaMaxLength = 1000;
  public readonly giftIdeaItemsMaxNumber = 5;
  public readonly inputMaxLength = 40;

  #radioControlSubscription!: Subscription;
  #formSubscription!: Subscription;

  ngOnInit(): void {
    this.#validateSelectedForm();
    this.#observeRadioControlValueChanges();
  }

  ngOnDestroy(): void {
    this.#radioControlSubscription?.unsubscribe();
    this.#formSubscription?.unsubscribe();
  }

  public get giftIdeas(): FormArray {
    return this.giftIdeaForm().get('wishList') as FormArray;
  }

  public get surpriseGiftAreaControl(): FormControl {
    return this.surpriseGiftForm().get('interests') as FormControl;
  }

  public addGiftIdea(): void {
    this.giftIdeas.push(
      this.#formBuilder.group({
        name: [''],
        infoLink: [
          '',
          {
            validators: [CustomValidators.safeUrl],
            updateOn: 'blur',
          },
        ],
      })
    );
  }

  public getGiftIdeaLinkControl(index: number): FormControl {
    return this.giftIdeas.at(index).get('infoLink') as FormControl;
  }

  public getGiftIdeaWishControl(index: number): FormControl {
    return this.giftIdeas.at(index).get('name') as FormControl;
  }

  public onFormCompleted(): void {
    this.formCompleted.emit();
  }

  #observeRadioControlValueChanges(): void {
    this.#radioControlSubscription = this.radioControl().valueChanges.subscribe(
      () => {
        this.#validateSelectedForm();
      }
    );
  }

  #validateSelectedForm(): void {
    const selectedForm = this.#getSelectedForm();

    if (selectedForm) {
      this.isFormValid.set(selectedForm.valid);
      this.#subscribeToFormStatus(selectedForm);
    }
  }

  #getSelectedForm(): FormGroup | null {
    if (this.radioControl().value === this.surpriseGiftRadioValue) {
      return this.surpriseGiftForm();
    }

    if (this.radioControl().value === this.giftIdeasRadioValue) {
      return this.giftIdeaForm();
    }

    return null;
  }

  #subscribeToFormStatus(form: FormGroup): void {
    this.#formSubscription?.unsubscribe();

    this.#formSubscription = form.statusChanges.subscribe(
      (status: FormControlStatus) => {
        this.isFormValid.set(status === 'VALID');
      }
    );
  }
}
