import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastService } from '../../core/services/toast-service';
import { themes } from '../theme';
import { BusyService } from '../../core/services/busy-service';
import { HasRole } from '../../core/directives/has-role';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive, HasRole],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav implements OnInit {
  protected accountService = inject(AccountService);
  protected busyService = inject(BusyService);
  protected creds: any = {};
  protected selectedTheme = signal<string>(localStorage.getItem('theme') || 'light');
  protected themes = themes;
  private router = inject(Router);
  private toastService = inject(ToastService);
  protected loading = signal(false);

  handleSelectTheme(theme: string) {
    console.log('Selected theme:', theme);
    this.selectedTheme.set(theme);
    localStorage.setItem('theme', theme);
    document.documentElement.setAttribute('data-theme', theme);
    const elem = document.activeElement as HTMLElement;
    if (elem) elem.blur();
  }

  ngOnInit(): void {
    document.documentElement.setAttribute('data-theme', this.selectedTheme());
  }

  handleSelectUserItem() {
    const elem = document.activeElement as HTMLElement;
    if (elem) elem.blur();
  }

  login() {
    this.loading.set(true);
    this.accountService.login(this.creds).subscribe({
      next: (response) => {
        console.log('Login successful', response);
        this.router.navigateByUrl('/members');
        this.toastService.success('Login successful!');
        this.creds = {};
      },
      error: (error) => {
        this.toastService.error(error.error);
        console.error('Login failed', error);
      },
      complete: () => this.loading.set(false),
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
