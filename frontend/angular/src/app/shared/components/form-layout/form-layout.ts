import { Component, computed, inject, input, output } from '@angular/core';

import { IMAGES_SPRITE_PATH } from '../../../app.constants';
import { Button } from '../button/button';
import {
  ButtonText,
  ButtonType,
  FormSubtitle,
  FormTitle,
  IconName,
  PictureName,
} from '../../../app.enum';
import { StepperManager } from '../../../core/services/stepper-manager';
import { BudgetPipe } from '../../pipes/budget.pipe';
import { BudgetControl } from '../../../app.models';

@Component({
  selector: 'app-form-layout',
  templateUrl: './form-layout.html',
  styleUrl: './form-layout.scss',
  imports: [Button, BudgetPipe],
})
export class FormLayout {
  readonly title = input.required<FormTitle>();
  readonly subtitle = input.required<FormSubtitle>();
  readonly formPictureName = input.required<PictureName>();

  readonly budget = input<BudgetControl>(0);
  readonly isFormValid = input<boolean>(false);

  readonly formCompleted = output<void>();

  readonly #stepperManagerService = inject(StepperManager);

  public readonly formPicturePositionClass = computed(
    () => `form__picture--${this.formPictureName()}`
  );
  public readonly formPictureHref = computed(
    () => `${IMAGES_SPRITE_PATH}#${this.formPictureName()}`
  );
  public readonly isFirstStep = computed(
    () => this.#stepperManagerService.currentStep() === 1
  );
  public readonly isLastStep = computed(
    () =>
      this.#stepperManagerService.maxSteps
      === this.#stepperManagerService.currentStep()
  );

  public readonly buttonIconName = IconName.ArrowLeft;
  public readonly backButtonText = ButtonText.BackToPrevStep;
  public readonly completeButtonText = ButtonText.Complete;
  public readonly continueButtonText = ButtonText.Continue;
  public readonly buttonTypeSubmit = ButtonType.Submit;

  public onNextStep(): void {
    this.#stepperManagerService.handleNextStep();
  }

  public onPreviousStep(): void {
    this.#stepperManagerService.handlePreviousStep();
  }

  public onComplete(e: MouseEvent): void {
    e.preventDefault();
    this.formCompleted.emit();
  }
}
