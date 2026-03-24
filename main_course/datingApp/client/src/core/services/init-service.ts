import { inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';
import { Observable, of } from 'rxjs';
import { LikesService } from './likes-service';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  private accountService = inject(AccountService);
  private likesService = inject(LikesService);

  init(): Observable<null> {
    const userJson = localStorage.getItem('user');
    if (!userJson) return of(null);
    const user = JSON.parse(userJson);
    this.accountService.currentUser.set(user);
    this.likesService.getLikeIds();

    return of(null); // Return an observable to indicate completion
  }
}
