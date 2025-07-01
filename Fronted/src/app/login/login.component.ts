import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'] // Optional, include your CSS file here
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  message: string | null = null;

  private apiUrl = 'https://localhost:7159/api/User/login'; // Replace with your actual API URL

  constructor(private http: HttpClient, private router: Router) {}

  login() {
    const loginData = {
      Username: this.username,
      Password: this.password,
    };

    this.http.post(this.apiUrl, loginData).subscribe({
      next: (response: any) => {
        // Handle successful response, e.g., navigate to dashboard
        console.log('Login successful', response);
        // You might want to store user data in local storage
        localStorage.setItem('user', JSON.stringify(response));
        this.router.navigate(['/dashboard']); // Redirect to dashboard or another page
      },
      error: (err) => {
        // Handle error response
        console.error('Login failed', err);
        this.message = 'Invalid username or password'; // Display error message
      }
    });
  }
}
