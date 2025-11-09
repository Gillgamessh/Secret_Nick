import { inject, Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';
import { HttpResponse } from '@angular/common/http';
import { tap } from 'rxjs';

import { ApiService } from '../../core/services/api';
import { Path } from '../../app.enum';
import {
  DEFAULT_ROOM_NAME,
  SUCCESS_PAGE_DATA_DEFAULT,
} from '../../app.constants';
import type {
  RoomCreationRequest,
  RoomSummary,
  CreateRoomSuccessPageData,
} from '../../app.models';

@Injectable()
export class CreateRoomService {
  readonly #successPageData = signal<CreateRoomSuccessPageData>(
    SUCCESS_PAGE_DATA_DEFAULT
  );

  public readonly successPageData = this.#successPageData.asReadonly();

  readonly #apiService = inject(ApiService);
  readonly #router = inject(Router);

  public processRoomCreation(roomCreationData: RoomCreationRequest): void {
    this.#apiService
      .createRoom(roomCreationData)
      .pipe(tap((response) => this.#handleRoomCreationResponse(response)))
      .subscribe();
  }

  #handleRoomCreationResponse(response: HttpResponse<RoomSummary>): void {
    if (response.status === 201) {
      const userCode = response?.body?.userCode || '';
      const invitationCode = response?.body?.room?.invitationCode || '';
      const invitationNote = response?.body?.room?.invitationNote || '';
      const name = response?.body?.room?.name || DEFAULT_ROOM_NAME;

      this.#successPageData.set({
        userCode,
        invitationCode,
        invitationNote,
        name,
      });

      void this.#router.navigate([Path.CreateRoom, Path.Success]);
    }
  }
}
