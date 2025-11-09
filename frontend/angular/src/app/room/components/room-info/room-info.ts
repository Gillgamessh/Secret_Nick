import { Component, computed, input, output } from '@angular/core';
import { RoomDataCard } from '../../../shared/components/room-data-card/room-data-card';
import {
  ButtonText,
  PictureName,
  RoomDataCardVariant,
  RoomInfoCardTitle,
} from '../../../app.enum';
import { DatePipe } from '@angular/common';
import { Button } from '../../../shared/components/button/button';
import { BudgetPipe } from '../../../shared/pipes/budget.pipe';

@Component({
  selector: 'app-room-info',
  imports: [RoomDataCard, DatePipe, Button, BudgetPipe],
  templateUrl: './room-info.html',
  styleUrl: './room-info.scss',
})
export class RoomInfo {
  readonly title = input.required<string>();
  readonly description = input.required<string>();
  readonly exchangeDateString = input.required<string>();
  readonly budget = input.required<number>();
  readonly invitationNote = input.required<string>();
  readonly invitationLink = input.required<string>();
  readonly isAdmin = input<boolean>(false);
  readonly isDrawn = input<boolean>(false);
  readonly userCode = input.required<string>();

  public readonly exchangeDate = computed(
    () => new Date(this.exchangeDateString())
  );
  public readonly noteWithLink = computed(
    () => `${this.invitationNote()}\n\n${this.invitationLink()}`
  );
  public readonly variant = RoomDataCardVariant.Light;
  public readonly presentsIcon = PictureName.BigPresents;
  public readonly starIcon = PictureName.Star2;
  public readonly letterIcon = PictureName.Letter;
  public readonly dataTitle = RoomInfoCardTitle.ExchangeDate;
  public readonly budgetTitle = RoomInfoCardTitle.GiftBudget;
  public readonly invitationTitle = RoomInfoCardTitle.InvitationNote;
  public readonly inviteButtonText = ButtonText.InviteNewMembers;

  readonly buttonAction = output<void>();

  public onButtonClick(): void {
    this.buttonAction.emit();
  }
}
