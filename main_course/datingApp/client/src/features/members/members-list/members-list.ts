import { Component, inject } from '@angular/core';
import { MemberService } from '../../../core/services/member-service';
import { Member } from '../../../types/member';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { MemberCard } from '../member-card/member-card';

@Component({
  selector: 'app-members-list',
  imports: [AsyncPipe, MemberCard],
  templateUrl: './members-list.html',
  styleUrl: './members-list.css',
})
export class MembersList {
  private memberService = inject(MemberService);
  members$: Observable<Member[]>;

  constructor() {
    this.members$ = this.memberService.getMembers();
  }
}
