import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

import { ToastService } from '../toast';
import { Endpoint, ToastMessage, MessageType } from '../../../app.enum';

export const errorHandlingInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
  const toasterService = inject(ToastService);

  return next(req).pipe(
    tap({
      error: (_error: HttpErrorResponse) => {
        const isRoomsRequest =
          req.method === 'GET'
          && req.url.includes(Endpoint.rooms)
          && req.params.has('roomCode');

        if (isRoomsRequest) {
          return;
        }

        toasterService.show(ToastMessage.SomethingWentWrong, MessageType.Error);
      },
    })
  );
};
