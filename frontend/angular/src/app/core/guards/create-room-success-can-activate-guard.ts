import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';

import { ToastMessage, MessageType, Path } from '../../app.enum';
import { CreateRoomService } from '../../create-room/services/create-room';
import { GuardReturnType } from '../../app.models';
import { NavigationService } from '../services/navigation';

export const createRoomSuccessCanActivateGuard: CanActivateFn = (
  _route,
  _state
): GuardReturnType => {
  const createRoomService = inject(CreateRoomService);
  const navigationService = inject(NavigationService);

  if (createRoomService.successPageData().userCode) {
    return true;
  }

  return navigationService.redirectWithToast(
    [Path.Home],
    ToastMessage.PleaseCreateYourRoom,
    MessageType.Error
  );
};
