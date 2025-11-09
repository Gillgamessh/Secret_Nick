import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Stepper } from '../shared/components/stepper/stepper';
import { CreateRoomForm } from '../components/forms/create-room-form/create-room-form';
import { StepperManager } from '../core/services/stepper-manager';
import { STEPPER_LABELS_TOKEN } from '../core/services/tokens/stepper-labels.token';
import { CREATE_ROOM_STEPPER_LABELS } from '../app.constants';
import { AddYourDetailsForm } from '../components/forms/add-your-details-form/add-your-details-form';
import { AddYourWishesForm } from '../components/forms/add-your-wishes-form/add-your-wishes-form';
import { CreateRoomService } from './services/create-room';
import { JoinRoom } from '../join-room/join-room';
import type {
  CreateRoomFormType,
  BasicRoomDetails,
  BudgetControl,
} from '../app.models';
import { FormValidation as CustomValidators } from '../core/services/form-validation';

@Component({
  selector: 'app-create-room',
  imports: [
    Stepper,
    CreateRoomForm,
    AddYourDetailsForm,
    AddYourWishesForm,
    ReactiveFormsModule,
  ],
  templateUrl: './create-room.html',
  styleUrl: './create-room.scss',
  providers: [
    {
      provide: STEPPER_LABELS_TOKEN,
      useValue: CREATE_ROOM_STEPPER_LABELS,
    },
    StepperManager,
  ],
})
export class CreateRoom extends JoinRoom implements OnInit {
  readonly #createRoomService = inject(CreateRoomService);
  readonly #nullableFormBuilder = inject(FormBuilder);

  public createRoomForm!: FormGroup<CreateRoomFormType>;

  override ngOnInit(): void {
    super.ngOnInit();
    this.createRoomForm = this.#initCreateRoomForm();
    this.giftMaximumBudget = this.createRoomForm.controls.giftMaximumBudget;
  }

  public override onFormCompleted(): void {
    const roomCreationData = {
      room: this.createRoomForm.value as BasicRoomDetails,
      adminUser: this.getUserDetails(),
    };
    this.#createRoomService.processRoomCreation(roomCreationData);
  }

  #initCreateRoomForm(): FormGroup<CreateRoomFormType> {
    return this.formBuilder.group({
      name: [
        '',
        {
          validators: [Validators.required],
          updateOn: 'blur',
        },
      ],
      description: [
        '',
        {
          validators: [Validators.required],
          updateOn: 'blur',
        },
      ],
      giftExchangeDate: [
        '',
        {
          validators: [Validators.required],
          updateOn: 'blur',
        },
      ],
      giftMaximumBudget: this.#nullableFormBuilder.control<BudgetControl>(
        null,
        {
          validators: [Validators.required, CustomValidators.budgetInRange],
          updateOn: 'blur',
        }
      ),
    });
  }
}
