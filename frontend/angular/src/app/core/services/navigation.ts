import { inject, Injectable } from '@angular/core';
import { Router, UrlTree } from '@angular/router';

import { ToastMessage, MessageType } from '../../app.enum';
import { ToastService } from './toast';

@Injectable({
  providedIn: 'root',
})
export class NavigationService {
  readonly #router = inject(Router);
  readonly #toaster = inject(ToastService);

  public redirectWithToast(
    route: string[],
    errorMessage: ToastMessage,
    type: MessageType
  ): UrlTree {
    this.#toaster.show(errorMessage, type);
    return this.#router.createUrlTree(route);
  }
}
