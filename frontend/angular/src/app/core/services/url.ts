import { inject, Injectable } from '@angular/core';

import type { NavigationLinks } from '../../app.models';

@Injectable({ providedIn: 'root' })
export class UrlService {
  readonly #window = inject(Window);
  readonly #origin = this.#window.location.origin;

  public getNavigationLinks(
    userCode: string,
    segment: string
  ): NavigationLinks {
    const code = encodeURIComponent(userCode.trim());
    const routerPath = `/${segment}/${code}`;
    const absoluteUrl = `${this.#origin}${routerPath}`;

    return { absoluteUrl, routerPath };
  }
}
