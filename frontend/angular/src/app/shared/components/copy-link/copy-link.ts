import { Component, HostBinding, inject, input } from '@angular/core';
import { IconButton } from '../icon-button/icon-button';
import {
  IconName,
  AriaLabel,
  CaptionMessage,
  CopyMessage,
  PopupPosition,
  MessageType,
  CopyLinkType,
} from '../../../app.enum';

import { copyToClipboard } from '../../../utils/copy';
import { PopupService } from '../../../core/services/popup';

@Component({
  selector: 'app-copy-link',
  imports: [IconButton],
  templateUrl: './copy-link.html',
  styleUrl: './copy-link.scss',
})
export class CopyLink {
  readonly link = input.required<string>();
  readonly type = input<CopyLinkType>(CopyLinkType.Dark);
  readonly caption = input<CaptionMessage>(CaptionMessage.EmptyMessage);
  private readonly popupService = inject(PopupService);

  copyIcon = IconName.Copy;
  ariaCopyButton = AriaLabel.CopyButton;
  popupPositionRight = PopupPosition.Right;
  successMessageType = MessageType.Success;
  errorMessageType = MessageType.Error;

  async onClick(event: MouseEvent) {
    const button = event.currentTarget as HTMLElement;
    const isCopied = await copyToClipboard(this.link());
    if (isCopied) {
      this.popupService.show(button, this.popupPositionRight, {
        message: CopyMessage.Success,
        type: this.successMessageType,
      });
    } else {
      this.popupService.show(button, this.popupPositionRight, {
        message: CopyMessage.Error,
        type: this.errorMessageType,
      });
    }
  }

  @HostBinding('class') get hostClass() {
    return `copy-link copy-link--${this.type()}`;
  }
}
