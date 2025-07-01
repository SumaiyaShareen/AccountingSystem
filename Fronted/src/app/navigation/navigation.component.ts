import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent {
  constructor(private router: Router) {}

  logout(): void {
    // Placeholder for actual logout logic (e.g., clearing tokens, session data)
    console.log('User logged out');
    this.router.navigate(['/login']);  // Redirects to login page after logout
  }
}
