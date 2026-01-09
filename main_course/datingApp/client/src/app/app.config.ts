import {
  ApplicationConfig,
  inject,
  provideAppInitializer,
  provideBrowserGlobalErrorListeners,
} from '@angular/core';
import { provideRouter, withViewTransitions } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { InitService } from '../core/services/init-service';
import { last, lastValueFrom, throwError } from 'rxjs';
import { errorInterceptor } from '../core/interceptors/error-interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes, withViewTransitions()),
    provideHttpClient(withInterceptors([errorInterceptor])),
    provideAppInitializer(async () => {
      const initService = inject(InitService);
      return new Promise<void>(async (resolve, reject) => {
        setTimeout(async () => {
          try {
            return await lastValueFrom(initService.init());
          } catch (error) {
            console.error('App initialization failed', error);
            throw error;
          } finally {
            const splash = document.getElementById('initial-splash');
            if (splash) {
              splash.remove();
            }
            resolve();
          }
        }, 500);
      });
    }),
  ],
};
