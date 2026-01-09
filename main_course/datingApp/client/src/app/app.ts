import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Nav } from '../layout/nav/nav';
import { User } from '../types/user';
import { Router, RouterOutlet } from '@angular/router';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [Nav, RouterOutlet, NgClass],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  // private accountService = inject(AccountService);
  private http = inject(HttpClient);
  protected router = inject(Router);
  protected readonly title = 'Dating App';
  protected members = signal<User[]>([]);

  // constructor(private http: HttpClient){} // Example of injecting HttpClient in the constructor

  async ngOnInit() {
    this.members.set(await this.getMembers());
    // this.setCurrentUser();
    // Using HttpClient to make a GET request using subscribe

    /*     this.http.get('https://localhost:7165/api/members').subscribe({
      next: (response) => {
        this.members.set(response as any[]);
      },
      error: (error) => {
        console.error(error);
      },

      complete: () => {
        console.log('Request completed');
      },
    }); */
  }

  //get current user

  // private setCurrentUser() {
  //   const userJson = localStorage.getItem('user');
  //   if (!userJson) return;
  //   const user = JSON.parse(userJson);
  //   this.accountService.currentUser.set(user);
  // }
  //get data with async/await

  async getMembers() {
    try {
      const response = lastValueFrom(this.http.get<User[]>('https://localhost:7165/api/members'));
      return response;
    } catch (error) {
      console.error(error);
      throw error;
    }
  }
}
