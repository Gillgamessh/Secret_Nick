import { computed, Directive, inject, input, output } from '@angular/core';

import {
  AriaLabel,
  ButtonText,
  IconName,
  ModalTitle,
  PictureName,
} from '../../app.enum';
import { IMAGES_SPRITE_PATH } from '../../app.constants';
import { ModalService } from '../services/modal';

@Directive()
export class ParentModalLayout {
  readonly headerPictureName = input.required<PictureName>();
  readonly headerTitle = input.required<ModalTitle>();
  readonly buttonText = input.required<ButtonText>();

  readonly #modalService = inject(ModalService);

  readonly isModalOpen = this.#modalService.isModalOpen;

  readonly closeModal = output<void>();
  readonly buttonAction = output<void>();

  public readonly headerPictureHref = computed(
    () => `${IMAGES_SPRITE_PATH}#${this.headerPictureName()}`
  );

  public readonly closeIcon = IconName.Close;
  public readonly closeButtonAriaLabel = AriaLabel.Close;

  public onCloseModal(): void {
    this.closeModal.emit();
  }

  public onActionButtonClick(): void {
    this.buttonAction.emit();
  }
}
