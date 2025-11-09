import { Component, inject, OnInit, Signal } from '@angular/core';
import { Router } from '@angular/router';

import { PageLayout } from '../shared/components/page-layout/page-layout';
import {
  ButtonText,
  ButtonType,
  CaptionMessage,
  NavigationLinkSegment,
  PageSubtitle,
  PageTitle,
  PictureName,
} from '../app.enum';
import { CopyLink } from '../shared/components/copy-link/copy-link';
import { InvitationNote } from '../shared/components/invitation-note/invitation-note';
import { UrlService } from '../core/services/url';
import { CreateRoomService } from './services/create-room';
import type { CreateRoomSuccessPageData } from '../app.models';

@Component({
  selector: 'app-success',
  imports: [PageLayout, CopyLink, InvitationNote],
  templateUrl: './success.html',
  styleUrl: './success.scss',
})
export class Success implements OnInit {
  readonly #router = inject(Router);
  readonly #urlService = inject(UrlService);
  readonly #createRoomService = inject(CreateRoomService);

  #successPageData!: Signal<CreateRoomSuccessPageData>;

  public roomName!: string;
  public invitationNote!: string;
  public personalLink!: string;
  public roomLink!: string;
  public routerPath!: string;
  public userCode!: string;
  public pageTitle!: PageTitle;

  public readonly btnText = ButtonText.Success;
  public readonly btnType = ButtonType.Button;
  public readonly doNotShare = CaptionMessage.DoNotShare;
  public readonly pageSubtitle = PageSubtitle.Success;
  public readonly pagePictureName = PictureName.Flat;
  public readonly invitationNoteMaxLength = 1000;

  ngOnInit(): void {
    this.#initSuccessPage();
  }

  public onButtonClick(): void {
    void this.#router.navigateByUrl(this.routerPath);
  }

  #initSuccessPage(): void {
    this.#successPageData = this.#createRoomService.successPageData;
    const pageData = this.#successPageData();
    const userCode = pageData.userCode;
    const roomCode = pageData.invitationCode;
    const pathsRoom = this.#urlService.getNavigationLinks(
      roomCode,
      NavigationLinkSegment.Join
    );
    const pathsPersonalLink = this.#urlService.getNavigationLinks(
      userCode,
      NavigationLinkSegment.Room
    );

    this.invitationNote = pageData.invitationNote;
    this.personalLink = pathsPersonalLink.absoluteUrl;
    this.userCode = pageData.userCode;
    this.roomLink = pathsRoom.absoluteUrl;
    this.routerPath = pathsPersonalLink.routerPath;
    this.roomName = pageData.name;
    this.pageTitle = `Your ${this.roomName} Room is Ready!` as PageTitle;
  }
}
