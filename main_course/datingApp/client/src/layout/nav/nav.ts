import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastService } from '../../core/services/toast-service';
import { themes } from '../theme';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav implements OnInit {
  protected accountService = inject(AccountService);
  protected creds: any = {};
  protected selectedTheme = signal<string>(localStorage.getItem('theme') || 'light');
  protected themes = themes;
  private router = inject(Router);
  private toastService = inject(ToastService);

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

  login() {
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
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
