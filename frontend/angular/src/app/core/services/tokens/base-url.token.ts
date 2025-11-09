import { InjectionToken } from '@angular/core';

import config from '../../../../../config.json';

export const BASE_URL = new InjectionToken<string>('BaseUrl', {
  providedIn: 'root',
  factory: () => {
    return config?.environment?.backendApiUrl;
  },
});
