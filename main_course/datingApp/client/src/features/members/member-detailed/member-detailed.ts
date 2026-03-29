import { Component, computed, inject, signal } from '@angular/core';
import { MemberService } from '../../../core/services/member-service';
import {
  ActivatedRoute,
  RouterLink,
  RouterOutlet,
  RouterLinkActive,
  Router,
  NavigationEnd,
} from '@angular/router';
import { filter } from 'rxjs';
import { AgePipe } from '../../../core/pipes/age-pipe';
import { AccountService } from '../../../core/services/account-service';

@Component({
  selector: 'app-member-detailed',
  imports: [RouterLink, RouterOutlet, RouterLinkActive, AgePipe],
  templateUrl: './member-detailed.html',
  styleUrl: './member-detailed.css',
})
export class MemberDetailed {
  private accountService = inject(AccountService);
  protected memberService = inject(MemberService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  protected title = signal('Profile');
  // protected member$: Observable<Member>; // Old way using service directly
  // protected member = signal<Member | null>(null);
  protected isCurrentUser = computed(
    () => this.accountService.currentUser()?.id === this.route.snapshot.paramMap.get('id'),
  );

  constructor() {
    const memberId = this.route.snapshot.paramMap.get('id')!;
    // this.member$ = this.memberService.getMember(memberId);// Old way using service directly
    // this.route.data.subscribe({
    //   next: (data) => {
    //     this.member.set(data['member']);
    //   },
    // });
    this.title.set(this.route.firstChild?.snapshot?.['title'] || 'Profile');
    this.router.events.pipe(filter((event) => event instanceof NavigationEnd)).subscribe({
      next: () => {
        this.title.set(this.route.firstChild?.snapshot?.['title'] || 'Profile');
      },
    });
  }
}
