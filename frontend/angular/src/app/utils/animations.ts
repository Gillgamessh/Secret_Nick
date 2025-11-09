import { animate, style, transition, trigger } from '@angular/animations';

export const fadeIn = (durationsMs: number) => {
  return trigger('dissolve', [
    transition(':enter', [
      style({ opacity: 0 }),
      animate(`${durationsMs}ms ease-out`, style({ opacity: 1 })),
    ]),
    transition(':leave', [
      style({ opacity: 1 }),
      animate(`${durationsMs}ms ease-in`, style({ opacity: 0 })),
    ]),
  ]);
};
