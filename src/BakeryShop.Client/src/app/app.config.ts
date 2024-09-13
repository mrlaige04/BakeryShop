import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {apiConfigProvider} from "./utils/providers/apiConfigProvider";
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {passTokenInterceptor} from "./auth/pass-token.interceptor";

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(
      withInterceptors([passTokenInterceptor]),
    ),
    apiConfigProvider
  ]
};
