import { Component, input, output, signal } from '@angular/core';
import {
  ButtonText,
  ModalSubtitle,
  ModalTitle,
  PictureName,
} from '../../../app.enum';
import { CommonModalTemplate } from '../modal/common-modal-template/common-modal-template';
import { CopyLink } from '../copy-link/copy-link';
import { InvitationNote } from '../invitation-note/invitation-note';

@Component({
  selector: 'app-invitation-modal',
  imports: [CommonModalTemplate, CopyLink, InvitationNote],
  templateUrl: './invitation-modal.html',
  styleUrl: './invitation-modal.scss',
})
export class InvitationModal {
  public readonly roomLink = input.required<string>();
  public readonly invitationNote = input.required<string>();
  public readonly userCode = input.required<string>();
  public readonly invitationNoteMaxLength = input<number>(1000);
  public readonly buttonAction = output<void>();
  public readonly closeModal = output<void>();

  public readonly headerPictureName = PictureName.Invitation;
  public readonly headerTitle = ModalTitle.Invitation;
  public readonly subtitle = ModalSubtitle.Invitation;
  public readonly goBackText = ButtonText.GoBackToRoom;

  public readonly currentInvitationNote = signal<string>('');

  public onCloseModal(): void {
    this.closeModal.emit();
  }

  public onButtonAction(): void {
    this.buttonAction.emit();
  }
}
