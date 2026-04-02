import { inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  private accountService = inject(AccountService);
  // private likesService = inject(LikesService);

  init() {
    // const userJson = localStorage.getItem('user');
    // if (!userJson) return of(null);
    // const user = JSON.parse(userJson);

    return this.accountService.refreshToken().pipe(
      tap((user) => {
        console.log('Token refreshed', user);
        if (user) {
          // this.accountService.currentUser.set(user);
          // this.likesService.getLikeIds();
          this.accountService.setCurrentUser(user);
          this.accountService.startTokenRefreshInterval();
        }
      }),
    );

    // return of(null); // Return an observable to indicate completion
  }
}
