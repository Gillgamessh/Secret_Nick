import { Component, computed, inject } from '@angular/core';
import { PageLayout } from '../shared/components/page-layout/page-layout';
import { RoomDataCard } from '../shared/components/room-data-card/room-data-card';
import {
  ButtonText,
  ButtonType,
  PageSubtitle,
  PageTitle,
  PictureName,
  RoomDataCardVariant,
  DateFormat,
} from '../app.enum';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { BudgetPipe } from '../shared/pipes/budget.pipe';
import { JoinRoomService } from '../join-room/services/join-room';

@Component({
  selector: 'app-welcome',
  imports: [PageLayout, RoomDataCard, DatePipe, BudgetPipe],
  templateUrl: './welcome.html',
  styleUrl: './welcome.scss',
})
export class Welcome {
  readonly #router = inject(Router);
  readonly #joinRoomService = inject(JoinRoomService);
  readonly #roomData = this.#joinRoomService.roomData;

  public readonly pageTitle =
    `${PageTitle.WelcomeJoin} ${this.#roomData().name}!` as PageTitle;
  public readonly pageSubtitle = PageSubtitle.WelcomeJoin;
  public readonly pagePictureName = PictureName.Firework;
  public readonly btnText = ButtonText.WelcomeJoin;
  public readonly btnType = ButtonType.Button;
  public readonly roomDataCardColorVariant = RoomDataCardVariant.Color;
  public readonly dateFormat = DateFormat.Short;

  readonly roomId = computed(() => this.#roomData().invitationCode);
  readonly giftExchangeDate = computed(() => this.#roomData().giftExchangeDate);
  readonly giftMaximumBudget = computed(
    () => this.#roomData().giftMaximumBudget
  );

  public onButtonClick(): void {
    void this.#router.navigate(['/join', this.roomId(), 'details']);
  }
}
