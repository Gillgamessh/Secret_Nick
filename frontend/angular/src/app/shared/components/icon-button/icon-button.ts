import { Component, computed, input } from '@angular/core';

import { ICONS_SPRITE_PATH } from '../../../app.constants';
import { AriaLabel, IconName } from '../../../app.enum';

@Component({
  selector: 'app-icon-button',
  templateUrl: './icon-button.html',
  styleUrl: './icon-button.scss',
})
export class IconButton {
  readonly iconName = input.required<IconName>();
  readonly ariaLabel = input.required<AriaLabel>();
  readonly isButtonDisabled = input<boolean>(false);

  readonly iconHref = computed(() => `${ICONS_SPRITE_PATH}#${this.iconName()}`);
}
