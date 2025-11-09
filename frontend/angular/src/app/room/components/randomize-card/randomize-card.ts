import { Component, computed, input, output } from '@angular/core';

import { ActionCardBase } from '../../../shared/components/action-card-base/action-card-base';
import { ActionCardTitle, ButtonText, PictureName } from '../../../app.enum';

@Component({
  selector: 'app-randomize-card',
  imports: [ActionCardBase],
  templateUrl: './randomize-card.html',
  styleUrl: './randomize-card.scss',
})
export class RandomizeCard {
  readonly isCardDisabled = input<boolean>(false);

  readonly buttonAction = output<void>();

  public readonly cardText = computed(() => this.#getCardText());

  public readonly headerTitle = ActionCardTitle.LetMagicBegin;
  public readonly headerPictureName = PictureName.BigGifts;
  public readonly buttonText = ButtonText.DrawNames;

  public onButtonClick(): void {
    this.buttonAction.emit();
  }

  #getCardText(): string {
    return this.isCardDisabled()
      ? 'You need at least 3 people in the room to enable drawing.'
      : 'Don’t forget to hit the button to randomly pair everyone in the game.';
  }
}
