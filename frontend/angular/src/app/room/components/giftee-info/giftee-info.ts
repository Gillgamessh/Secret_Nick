import { Component, input, output } from '@angular/core';

import { ActionCardBase } from '../../../shared/components/action-card-base/action-card-base';
import { ActionCardTitle, ButtonText, PictureName } from '../../../app.enum';

@Component({
  selector: 'app-giftee-info',
  imports: [ActionCardBase],
  templateUrl: './giftee-info.html',
  styleUrl: './giftee-info.scss',
})
export class GifteeInfo {
  readonly gifteeName = input.required<string>();

  readonly buttonAction = output<void>();

  public readonly headerTitle = ActionCardTitle.LookWhoYouGot;
  public readonly headerPictureName = PictureName.StNick;
  public readonly buttonText = ButtonText.ReadDetails;

  public onButtonClick(): void {
    this.buttonAction.emit();
  }
}
