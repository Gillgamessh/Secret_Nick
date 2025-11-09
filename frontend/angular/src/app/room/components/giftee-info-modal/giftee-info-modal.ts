import { Component, input, output } from '@angular/core';

import { ButtonText, ModalTitle, PictureName } from '../../../app.enum';
import { ModalLayoutWithHeader } from '../../../shared/components/modal/modal-layout-with-header/modal-layout-with-header';
import { PersonalInfo } from '../personal-info/personal-info';
import { ItemCard } from '../my-wishlist/components/item-card/item-card';
import { SurpriseInfo } from '../surprise-info/surprise-info';
import type {
  GifteePersonalInfoItem,
  GifteeWishlistInfo,
} from '../../../app.models';

@Component({
  selector: 'app-giftee-info-modal',
  imports: [ModalLayoutWithHeader, PersonalInfo, ItemCard, SurpriseInfo],
  templateUrl: './giftee-info-modal.html',
  styleUrl: './giftee-info-modal.scss',
})
export class GifteeInfoModal {
  readonly personalInfo = input.required<GifteePersonalInfoItem[]>();
  readonly wishListInfo = input.required<GifteeWishlistInfo>();

  readonly closeModal = output<void>();
  readonly buttonAction = output<void>();

  public readonly headerPictureName = PictureName.StNick;
  public readonly headerTitle = ModalTitle.LookWhoYouGot;
  public readonly buttonText = ButtonText.GoBackToRoom;

  public onCloseModal(): void {
    this.closeModal.emit();
  }

  public onActionButtonClick(): void {
    this.buttonAction.emit();
  }
}
