import { Component, input, output } from '@angular/core';

import { CommonModalTemplate } from '../../../shared/components/modal/common-modal-template/common-modal-template';
import {
  ButtonText,
  CaptionMessage,
  ModalSubtitle,
  ModalTitle,
  PersonalInfoTerm,
  PictureName,
} from '../../../app.enum';
import { PersonalInfo } from '../personal-info/personal-info';
import { CopyLink } from '../../../shared/components/copy-link/copy-link';
import type { GifteePersonalInfoItem } from '../../../app.models';

@Component({
  selector: 'app-participant-info-modal',
  imports: [CommonModalTemplate, PersonalInfo, CopyLink],
  templateUrl: './participant-info-modal.html',
  styleUrl: './participant-info-modal.scss',
})
export class ParticipantInfoModal {
  readonly personalInfo = input.required<GifteePersonalInfoItem[]>();
  readonly roomLink = input.required<string>();

  readonly closeModal = output<void>();
  readonly buttonAction = output<void>();

  public readonly pictureName = PictureName.Cookie;
  public readonly title = ModalTitle.ParticipantDetails;
  public readonly buttonText = ButtonText.GoBackToRoom;
  public readonly subtitle = ModalSubtitle.ParticipantInfo;
  public readonly roomLinkTerm = PersonalInfoTerm.RoomLink;
  public readonly shareWithParticipant = CaptionMessage.ShareWithParticipant;

  public onCloseModal(): void {
    this.closeModal.emit();
  }

  public onActionButtonClick(): void {
    this.buttonAction.emit();
  }
}
