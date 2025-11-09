import { Component, computed, input } from '@angular/core';
import { IconName, AriaLabel, ButtonType } from '../../../app.enum';
import { ICONS_SPRITE_PATH } from '../../../app.constants';

@Component({
  selector: 'app-button',
  templateUrl: './button.html',
  styleUrls: ['./button.scss'],
})
export class Button {
  readonly text = input.required<string>();
  readonly iconName = input<IconName>();
  readonly isIconRight = input<boolean>(false);
  readonly isDisabled = input<boolean>(false);
  readonly ariaLabel = input<AriaLabel>();
  readonly type = input<ButtonType>(ButtonType.Button);

  readonly iconHref = computed(() => `${ICONS_SPRITE_PATH}#${this.iconName()}`);
}
