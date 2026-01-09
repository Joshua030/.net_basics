import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError } from 'rxjs';
import { ToastService } from '../services/toast-service';
import { Router } from '@angular/router';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastService = inject(ToastService);
  const router = inject(Router);
  return next(req).pipe(
    catchError((error) => {
      if (error) {
        switch (error.status) {
          case 400:
            if (error.error.errors) {
              const modalStateErrors = [];
              for (const key in error.error.errors) {
                if (error.error.errors[key]) {
                  modalStateErrors.push(error.error.errors[key]);
                }
              }
              throw modalStateErrors.flat();
            } else {
              toastService.error(error.error || 'Bad Request');
            }
            break;
          case 401:
            toastService.error('Unauthorized');
            break;
          case 404:
            toastService.error('Not Found');
            router.navigateByUrl('/not-found');
            break;
          case 500:
            const navigationExtras = { state: { error: error.error } };
            router.navigateByUrl('/server-error', navigationExtras);
            break;

          default:
            toastService.error('Something unexpected went wrong');
            break;
        }
      }
      throw error;
    }),
  );
};
