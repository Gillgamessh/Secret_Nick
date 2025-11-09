import { inject, Injectable, signal } from '@angular/core';
import { STEPPER_LABELS_TOKEN } from './tokens/stepper-labels.token';
import { StepLabel } from '../../app.enum';

@Injectable()
export class StepperManager {
  readonly #currentStep = signal(1);

  public readonly currentStep = this.#currentStep.asReadonly();

  public readonly labels: StepLabel[] = inject(STEPPER_LABELS_TOKEN);

  public readonly maxSteps: number = this.labels.length;

  public handleNextStep(): void {
    this.#currentStep.update((step) => step + 1);
  }

  public handlePreviousStep(): void {
    this.#currentStep.update((step) => step - 1);
  }
}
