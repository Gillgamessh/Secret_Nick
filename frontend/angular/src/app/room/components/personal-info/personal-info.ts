import { Component, input } from '@angular/core';
import { IMAGES_SPRITE_PATH } from '../../../app.constants';
import type { GifteePersonalInfoItem } from '../../../app.models';

@Component({
  selector: 'app-personal-info',
  templateUrl: './personal-info.html',
  styleUrl: './personal-info.scss',
})
export class PersonalInfo {
  readonly personalInfo = input<GifteePersonalInfoItem[]>([]);
  readonly showBackgroundPicture = input<boolean>(true);

  public readonly pictureHref = `${IMAGES_SPRITE_PATH}#four-snowflakes`;
}
