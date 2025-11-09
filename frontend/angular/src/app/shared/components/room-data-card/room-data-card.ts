import { Component, computed, input } from '@angular/core';
import { RoomDataCardVariant } from '../../../app.enum';
import { IMAGES_SPRITE_PATH } from '../../../app.constants';

@Component({
  selector: 'app-room-data-card',
  templateUrl: './room-data-card.html',
  styleUrl: './room-data-card.scss',
  host: {
    '[class]': 'hostClass()',
  },
})
export class RoomDataCard {
  iconName = input.required<string>();
  title = input.required<string>();
  variant = input<RoomDataCardVariant>(RoomDataCardVariant.Color);

  public readonly hostClass = computed(
    () => `room-data-card room-data-card--${this.variant()}`
  );
  public readonly iconHref = computed(
    () => `${IMAGES_SPRITE_PATH}#${this.iconName()}`
  );
}
