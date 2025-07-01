import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-user-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class UserRegistrationComponent {
  username: string = '';
  passwordHash: string = '';
  role: string = 'User'; // Default role
  message: string = '';

  constructor(private http: HttpClient) {}

  register() {
    const userData = { username: this.username, passwordHash: this.passwordHash, role: this.role };

    this.http.post('https://localhost:7159/api/User', userData).subscribe(
      (response) => {
        this.message = 'Registration successful! Please login.';
        console.log(response);
      },
      (error) => {
        console.error('Registration failed:', error);
        if (error.status === 400) {
          this.message = 'Bad request: Please check your input and try again.';
        } else {
          this.message = 'An error occurred. Please try again later.';
        }
      }
    );
  }
}
