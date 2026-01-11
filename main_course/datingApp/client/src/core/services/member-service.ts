import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member, Photo } from '../../types/member';

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'members');
  }

  getMember(id: string) {
    // return this.http.get(this.baseUrl + 'members/' + id, this.getHttpOptions());
    return this.http.get<Member>(this.baseUrl + 'members/' + id);
  }

  getMemberPhotos(id: string) {
    return this.http.get<Photo[]>(this.baseUrl + 'members/' + id + '/photos');
  }

  // It is not necessary to send the token in the headers because the jwtInterceptor does it automatically
  /*  private getHttpOptions() {
    return {
      headers: {
        Authorization: 'Bearer ' + this.accountService.currentUser()?.token,
      },
    };
  } */
}
