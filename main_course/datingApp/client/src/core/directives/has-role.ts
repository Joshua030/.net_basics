import { Directive, inject, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { AccountService } from '../services/account-service';

@Directive({
  selector: '[appHasRole]',
})
export class HasRole implements OnInit {
  @Input() appHasRole: string[] = [];
  private accountService = inject(AccountService);
  private viewContainerref = inject(ViewContainerRef);
  private templateRef = inject(TemplateRef);

  ngOnInit(): void {
    if (this.accountService.currentUser()?.roles.some((r) => this.appHasRole.includes(r))) {
      this.viewContainerref.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainerref.clear();
    }
  }
}
