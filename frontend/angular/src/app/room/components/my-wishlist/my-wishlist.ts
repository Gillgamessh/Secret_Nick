import { Component, input, output } from '@angular/core';
import { ActionCardBase } from '../../../shared/components/action-card-base/action-card-base';
import { ActionCardTitle, ButtonText } from '../../../app.enum';
import { ItemCard } from './components/item-card/item-card';
import { User } from '../../../app.models';

@Component({
  selector: 'app-my-wishlist',
  imports: [ActionCardBase, ItemCard],
  templateUrl: './my-wishlist.html',
  styleUrl: './my-wishlist.scss',
})
export class MyWishlist {
  public readonly currentUser = input<User>();

  readonly buttonAction = output<void>();

  public readonly headerTitle = ActionCardTitle.MyWishlist;
  public readonly buttonText = ButtonText.ViewWishlist;

  public onButtonClick(): void {
    this.buttonAction.emit();
  }
}
