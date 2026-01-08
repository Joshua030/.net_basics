import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private http = inject(HttpClient);
  protected readonly title = 'Dating App';
  protected members = signal<any[]>([]);

  // constructor(private http: HttpClient){} // Example of injecting HttpClient in the constructor

  async ngOnInit() {
    this.members.set((await this.getMembers()) as any[]);
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

  //get data with async/await

  async getMembers() {
    try {
      const response = lastValueFrom(this.http.get('https://localhost:7165/api/members'));
      return response;
    } catch (error) {
      console.error(error);
      throw error;
    }
  }
}
