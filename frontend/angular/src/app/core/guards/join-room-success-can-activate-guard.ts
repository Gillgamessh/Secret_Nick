import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';

import { ToastMessage, MessageType, Path } from '../../app.enum';
import { JoinRoomService } from '../../join-room/services/join-room';
import { NavigationService } from '../services/navigation';
import type { GuardReturnType } from '../../app.models';

export const joinRoomSuccessCanActivateGuard: CanActivateFn = (
  _route,
  _state
): GuardReturnType => {
  const joinRoomService = inject(JoinRoomService);
  const navigationService = inject(NavigationService);

  if (joinRoomService.userCode()) {
    return true;
  }

  return navigationService.redirectWithToast(
    [Path.Join, joinRoomService.roomData().invitationCode],
    ToastMessage.PleaseJoinTheRoom,
    MessageType.Error
  );
};
