import { Component, input, output } from '@angular/core';
import { CommonModalTemplate } from '../../../../../shared/components/modal/common-modal-template/common-modal-template';
import { ButtonText, ModalTitle, PictureName } from '../../../../../app.enum';
import { GifteeWishlistInfo, WishListItem } from '../../../../../app.models';
import { ItemCard } from '../item-card/item-card';
import { SurpriseInfo } from '../../../surprise-info/surprise-info';

@Component({
  selector: 'app-my-wishlist-modal',
  imports: [CommonModalTemplate, ItemCard, SurpriseInfo],
  templateUrl: './my-wishlist-modal.html',
  styleUrl: './my-wishlist-modal.scss',
})
export class MyWishlistModal {
  readonly wishListInfo = input.required<GifteeWishlistInfo>();

  public readonly budget = input<number>();

  readonly closeModal = output<void>();
  readonly buttonAction = output<void>();

  public readonly picName = PictureName.BigPresents;
  public readonly title = ModalTitle.WishList;
  public readonly buttonText = ButtonText.GoBackToRoom;
  public readonly subtitle =
    'Let your Secret Nick know what would make you smile this season.';

  public onCloseModal(): void {
    this.closeModal.emit();
  }

  public onButtonClick(): void {
    this.buttonAction.emit();
  }
}
