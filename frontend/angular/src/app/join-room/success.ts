import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { PageLayout } from '../shared/components/page-layout/page-layout';
import { CopyLink } from '../shared/components/copy-link/copy-link';
import { JoinRoomService } from './services/join-room';
import { UrlService } from '../core/services/url';
import {
  ButtonText,
  ButtonType,
  CaptionMessage,
  NavigationLinkSegment,
  PageSubtitle,
  PageTitle,
  PictureName,
} from '../app.enum';

@Component({
  selector: 'app-success:not(p)',
  imports: [PageLayout, CopyLink],
  templateUrl: './success.html',
  styleUrl: './success.scss',
})
export class Success implements OnInit {
  readonly #router = inject(Router);
  readonly #urlService = inject(UrlService);
  readonly #joinRoomService = inject(JoinRoomService);

  public personalLink!: string;
  #routerPath!: string;

  public readonly pageTitle = PageTitle.YouHaveJoinedTheRoom;
  public readonly pageSubtitle = PageSubtitle.SuccessJoin;
  public readonly pagePictureName = PictureName.Flat;
  public readonly btnText = ButtonText.Success;
  public readonly btnType = ButtonType.Button;
  public readonly doNotShare = CaptionMessage.DoNotShare;

  ngOnInit(): void {
    this.#prepareNavigationPaths();
  }

  public onButtonClick(): void {
    void this.#router.navigateByUrl(this.#routerPath);
  }

  #prepareNavigationPaths(): void {
    const pathsPersonalLink = this.#urlService.getNavigationLinks(
      this.#joinRoomService.userCode(),
      NavigationLinkSegment.Room
    );

    this.personalLink = pathsPersonalLink.absoluteUrl;
    this.#routerPath = pathsPersonalLink.routerPath;
  }
}
