import { HttpEvent, HttpInterceptorFn, HttpParams } from '@angular/common/http';
import { BusyService } from '../services/busy-service';
import { inject } from '@angular/core';
import { delay, finalize, identity, of, tap } from 'rxjs';
import { environment } from '../../environments/environment';

type CacheEntry = {
  timestamp: number;
  response: HttpEvent<unknown>;
};

const cache = new Map<string, CacheEntry>();
const CACHE_DURATION = 5 * 60 * 1000; // 5 minutes

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusyService);

  const generateCacheKey = (url: string, params: HttpParams): string => {
    const paramString = params
      .keys()
      .map((key) => `${key}=${params.get(key)}`)
      .join('&');
    return paramString ? `${url}?${paramString}` : url;
  };

  const invalidateCache = (urlPattern: string) => {
    for (const key of cache.keys()) {
      if (key.includes(urlPattern)) {
        cache.delete(key);
      }
    }
  };

  const cachekey = generateCacheKey(req.url, req.params);

  if (req.method.includes('POST') && req.url.includes('/likes')) {
    invalidateCache('/likes');
  }

  if (req.method.includes('POST') && req.url.includes('/messages')) {
    invalidateCache('/messages');
  }

  if (req.method.includes('POST') && req.url.includes('/logout')) {
    cache.clear();
  }

  if (req.method === 'GET') {
    const cachedResponse = cache.get(cachekey);
    if (cachedResponse) {
      const isExpired = Date.now() - cachedResponse.timestamp > CACHE_DURATION;
      if (!isExpired) {
        return of(cachedResponse.response);
      } else {
        cache.delete(cachekey);
      }
    }
  }

  busyService.busy();
  return next(req).pipe(
    environment.production ? identity : delay(500),
    tap((event) => {
      if (req.method === 'GET') {
        cache.set(cachekey, { timestamp: Date.now(), response: event });
      }
    }),
    finalize(() => busyService.idle()),
  );
};
