import { InjectionToken } from '@angular/core';

import { StepLabel } from '../../../app.enum';

export const STEPPER_LABELS_TOKEN = new InjectionToken<StepLabel[]>(
  'STEPPER_LABELS_TOKEN'
);
