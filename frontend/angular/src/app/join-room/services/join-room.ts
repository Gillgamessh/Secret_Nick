import { inject, Injectable, signal } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { ApiService } from '../../core/services/api';
import { JOIN_ROOM_DATA_DEFAULT } from '../../app.constants';
import { Path } from '../../app.enum';
import type {
  JoinRoomWelcomePageData,
  RoomDetails,
  UserDetails,
} from '../../app.models';

@Injectable({
  providedIn: 'root',
})
export class JoinRoomService {
  readonly #apiService = inject(ApiService);
  readonly #router = inject(Router);

  readonly #roomData = signal<JoinRoomWelcomePageData>(JOIN_ROOM_DATA_DEFAULT);
  readonly #userCode = signal<string>('');

  public readonly roomData = this.#roomData.asReadonly();
  public readonly userCode = this.#userCode.asReadonly();

  public processAddingUserToRoom(userData: UserDetails): void {
    const roomCode = this.#roomData().invitationCode;

    this.#apiService
      .addUserToRoom(roomCode, userData)
      .pipe(
        tap(({ status, body }) => {
          const userCode = body?.userCode;

          if (status === 201 && userCode) {
            this.#userCode.set(userCode);

            void this.#router.navigate([Path.Join, roomCode, Path.Success]);
          }
        })
      )
      .subscribe();
  }

  public getRoomByRoomCode(
    roomId: string
  ): Observable<HttpResponse<RoomDetails>> {
    return this.#apiService.getRoomByRoomCode(roomId).pipe(
      tap(({ body }) => {
        if (body) {
          this.#roomData.set(body);
        }
      })
    );
  }
}
