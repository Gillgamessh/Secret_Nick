import { Component, input, output } from '@angular/core';
import { CopyLink } from '../../../shared/components/copy-link/copy-link';
import { ButtonText, CaptionMessage, CopyLinkType } from '../../../app.enum';
import { Button } from '../../../shared/components/button/button';

@Component({
  selector: 'app-personal-info',
  imports: [CopyLink, Button],
  templateUrl: './personal-info-card.html',
  styleUrl: './personal-info-card.scss',
})
export class PersonalInfoCard {
  public readonly firstName = input.required<string>();
  public readonly roomName = input.required<string>();
  public readonly link = input.required<string>();

  readonly buttonAction = output<void>();

  public readonly linkType = CopyLinkType.Light;
  public readonly caption = CaptionMessage.DoNotShare;
  public readonly buttonText = ButtonText.ViewInformation;

  public onButtonClick(): void {
    this.buttonAction.emit();
  }
}
