import { Component, computed, input, output } from '@angular/core';

import { Button } from '../button/button';
import { ActionCardTitle, ButtonText, PictureName } from '../../../app.enum';
import { IMAGES_SPRITE_PATH } from '../../../app.constants';

@Component({
  selector: 'app-action-card-base',
  imports: [Button],
  templateUrl: './action-card-base.html',
  styleUrl: './action-card-base.scss',
})
export class ActionCardBase {
  readonly headerTitle = input.required<ActionCardTitle>();
  readonly buttonText = input.required<ButtonText>();

  readonly headerPictureName = input<PictureName>();
  readonly isButtonDisabled = input<boolean>(false);

  readonly buttonAction = output<void>();

  public readonly headerPictureHref = computed(
    () => `${IMAGES_SPRITE_PATH}#${this.headerPictureName()}`
  );
  public readonly pictureClass = computed(() => {
    return this.headerPictureName()
      ? `action-card--${this.headerPictureName()}`
      : '';
  });

  public onButtonClick(): void {
    this.buttonAction.emit();
  }
}
