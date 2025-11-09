import {
  ApplicationConfig,
  provideBrowserGlobalErrorListeners,
  provideZonelessChangeDetection,
  importProvidersFrom,
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { en_US, provideNzI18n } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { FormsModule } from '@angular/forms';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

import { routes } from './app.routes';
import { httpHeadersInterceptor } from './core/services/interceptors/http-headers-interceptor';
import { loaderInterceptor } from './core/services/interceptors/loader-interceptor';
import { errorHandlingInterceptor } from './core/services/interceptors/error-handling-interceptor';

registerLocaleData(en);

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZonelessChangeDetection(),
    provideRouter(routes),
    provideNzI18n(en_US),
    importProvidersFrom(FormsModule),
    provideAnimations(),
    provideHttpClient(
      withInterceptors([
        loaderInterceptor,
        httpHeadersInterceptor,
        errorHandlingInterceptor,
      ])
    ),
    {
      provide: Window,
      useValue: window,
    },
  ],
};
