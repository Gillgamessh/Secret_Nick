import { Component, computed, input, output } from '@angular/core';
import {
  ButtonText,
  ButtonType,
  PageSubtitle,
  PageTitle,
  PictureName,
} from '../../../app.enum';
import { IMAGES_SPRITE_PATH } from '../../../app.constants';
import { Button } from '../button/button';

@Component({
  selector: 'app-page-layout',
  imports: [Button],
  templateUrl: './page-layout.html',
  styleUrl: './page-layout.scss',
})
export class PageLayout {
  readonly title = input.required<PageTitle>();
  readonly subtitle = input.required<PageSubtitle>();
  readonly pagePictureName = input.required<PictureName>();

  readonly buttonText = input<ButtonText>(ButtonText.Success);
  readonly buttonType = input<ButtonType>(ButtonType.Button);

  readonly buttonClick = output<void>();

  public readonly layoutPictureHref = computed(
    () => `${IMAGES_SPRITE_PATH}#${this.pagePictureName()}`
  );
  public readonly layoutPicturePositionClass = computed(
    () => `page-layout__picture--${this.pagePictureName()}`
  );
}
