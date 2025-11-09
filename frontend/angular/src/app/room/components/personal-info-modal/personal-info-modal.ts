import { Component, input, output } from '@angular/core';

import { CommonModalTemplate } from '../../../shared/components/modal/common-modal-template/common-modal-template';
import {
  ButtonText,
  ModalSubtitle,
  ModalTitle,
  PictureName,
} from '../../../app.enum';
import { PersonalInfo } from '../personal-info/personal-info';
import type { GifteePersonalInfoItem } from '../../../app.models';

@Component({
  selector: 'app-personal-info-modal',
  imports: [CommonModalTemplate, PersonalInfo],
  templateUrl: './personal-info-modal.html',
})
export class PersonalInfoModal {
  readonly personalInfo = input.required<GifteePersonalInfoItem[]>();

  readonly closeModal = output<void>();
  readonly buttonAction = output<void>();

  public readonly pictureName = PictureName.Car;
  public readonly title = ModalTitle.PersonalInformation;
  public readonly buttonText = ButtonText.GoBackToRoom;
  public readonly subtitle = ModalSubtitle.PersonalInfo;

  public onCloseModal(): void {
    this.closeModal.emit();
  }

  public onActionButtonClick(): void {
    this.buttonAction.emit();
  }
}
