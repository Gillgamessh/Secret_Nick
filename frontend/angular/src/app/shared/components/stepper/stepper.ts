import { Component, input } from '@angular/core';

import { ICONS_SPRITE_PATH } from '../../../app.constants';
import { IconName, StepLabel } from '../../../app.enum';

@Component({
  selector: 'app-stepper',
  templateUrl: './stepper.html',
  styleUrl: './stepper.scss',
})
export class Stepper {
  stepLabels = input.required<StepLabel[]>();
  stepsCount = input.required<number>();
  currentStep = input.required<number>();

  successIconHref = `${ICONS_SPRITE_PATH}#${IconName.SuccessMark}`;
}
