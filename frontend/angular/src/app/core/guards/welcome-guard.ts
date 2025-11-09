import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { catchError, map, of } from 'rxjs';

import { ToastMessage, MessageType, Path } from '../../app.enum';
import { JoinRoomService } from '../../join-room/services/join-room';
import { NavigationService } from '../services/navigation';
import type { GuardReturnType } from '../../app.models';

export const welcomeGuard: CanActivateFn = (route): GuardReturnType => {
  const roomApi = inject(JoinRoomService);
  const navigationService = inject(NavigationService);

  return roomApi.getRoomByRoomCode(route.params['roomId']).pipe(
    map((result) => {
      if (!result.body?.closedOn && !result.body?.isFull) {
        return true;
      }

      const errorMessage: ToastMessage = result.body?.closedOn
        ? ToastMessage.UnavailableRoom
        : ToastMessage.FullRoom;

      return navigationService.redirectWithToast(
        [Path.Home],
        errorMessage,
        MessageType.Error
      );
    }),
    catchError(() =>
      of(
        navigationService.redirectWithToast(
          [Path.Home],
          ToastMessage.UnavailableRoom,
          MessageType.Error
        )
      )
    )
  );
};
