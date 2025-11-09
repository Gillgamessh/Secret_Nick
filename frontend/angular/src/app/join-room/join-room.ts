import { Component, inject, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { Stepper } from '../shared/components/stepper/stepper';
import { AddYourDetailsForm } from '../components/forms/add-your-details-form/add-your-details-form';
import { AddYourWishesForm } from '../components/forms/add-your-wishes-form/add-your-wishes-form';
import { STEPPER_LABELS_TOKEN } from '../core/services/tokens/stepper-labels.token';
import { StepperManager } from '../core/services/stepper-manager';
import { JOIN_ROOM_STEPPER_LABELS, PHONE_CODE_UA } from '../app.constants';
import { JoinRoomService } from './services/join-room';
import { RadioButtonValue } from '../app.enum';
import { FormValidation as CustomValidators } from '../core/services/form-validation';
import type {
  AddYourDetailsFormType,
  BasicUserDetails,
  GiftIdeaFormType,
  SurpriseGiftFormType,
  UserDetails,
  WishListItem,
} from '../app.models';

@Component({
  selector: 'app-join-room',
  templateUrl: './join-room.html',
  styleUrl: './join-room.scss',
  imports: [
    ReactiveFormsModule,
    Stepper,
    AddYourDetailsForm,
    AddYourWishesForm,
  ],
  providers: [
    {
      provide: STEPPER_LABELS_TOKEN,
      useValue: JOIN_ROOM_STEPPER_LABELS,
    },
    StepperManager,
  ],
})
export class JoinRoom implements OnInit {
  public readonly formBuilder = inject(NonNullableFormBuilder);
  readonly #stepperManagerService = inject(StepperManager);
  readonly #joinRoomService = inject(JoinRoomService);

  public readonly currentStep = this.#stepperManagerService.currentStep;
  public readonly stepsCount = this.#stepperManagerService.maxSteps;
  public readonly stepLabels = this.#stepperManagerService.labels;

  public readonly radioControl = new FormControl();

  public addYourDetailsForm!: FormGroup<AddYourDetailsFormType>;
  public giftIdeaForm!: FormGroup<GiftIdeaFormType>;
  public surpriseGiftForm!: FormGroup<SurpriseGiftFormType>;

  public giftMaximumBudget = new FormControl();

  ngOnInit(): void {
    const giftMaximumBudget =
      this.#joinRoomService.roomData().giftMaximumBudget;
    this.giftMaximumBudget.setValue(giftMaximumBudget);
    this.addYourDetailsForm = this.#initAddYourDetailsForm();
    this.giftIdeaForm = this.#initGiftIdeaForm();
    this.surpriseGiftForm = this.#initSurpriseGiftForm();
  }

  public onFormCompleted(): void {
    const userData = this.getUserDetails();
    this.#joinRoomService.processAddingUserToRoom(userData);
  }

  public getUserDetails(): UserDetails {
    const wantSurprise =
      this.radioControl.value === RadioButtonValue.SurpriseGift;
    const phone = `${PHONE_CODE_UA}${this.addYourDetailsForm.value.phone}`;
    const wishList = !wantSurprise
      ? (this.giftIdeaForm.value.wishList as WishListItem[])
      : [];
    const interests = wantSurprise
      ? (this.surpriseGiftForm.value.interests as string)
      : '';

    return {
      ...(this.addYourDetailsForm.value as BasicUserDetails),
      phone,
      wantSurprise,
      wishList,
      interests,
    };
  }

  #initAddYourDetailsForm(): FormGroup<AddYourDetailsFormType> {
    return this.formBuilder.group({
      firstName: [
        '',
        {
          validators: [Validators.required],
          updateOn: 'blur',
        },
      ],
      lastName: [
        '',
        {
          validators: [Validators.required],
          updateOn: 'blur',
        },
      ],
      phone: [
        '',
        {
          validators: [Validators.required, CustomValidators.phone],
          updateOn: 'blur',
        },
      ],
      email: [
        '',
        {
          validators: [Validators.email],
          updateOn: 'blur',
        },
      ],
      deliveryInfo: [
        '',
        {
          validators: [Validators.required],
          updateOn: 'blur',
        },
      ],
    });
  }

  #initGiftIdeaForm(): FormGroup<GiftIdeaFormType> {
    return this.formBuilder.group({
      wishList: this.formBuilder.array([
        this.formBuilder.group({
          name: [
            '',
            {
              validators: [Validators.required],
              updateOn: 'blur',
            },
          ],
          infoLink: [
            '',
            {
              validators: [CustomValidators.safeUrl],
              updateOn: 'blur',
            },
          ],
        }),
      ]),
    });
  }

  #initSurpriseGiftForm(): FormGroup<SurpriseGiftFormType> {
    return this.formBuilder.group({
      interests: [
        '',
        {
          validators: [Validators.required],
          updateOn: 'blur',
        },
      ],
    });
  }
}
