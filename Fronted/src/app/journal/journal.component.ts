import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-journal-entry',
  templateUrl: './journal.component.html',
  styleUrls: ['./journal.component.css']
})
export class JournalComponent implements OnInit {
  journalEntryForm!: FormGroup;
  journalEntries: any[] = [];
  users: any[] = [];
  isLoading = false;
  errorMessage: string | null = null;
  
  private readonly apiUrl = 'https://localhost:7159/api';

  constructor(private fb: FormBuilder, private http: HttpClient) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadUsers();
    this.loadJournalEntries();
  }

  // Initialize the journal entry form
  private initializeForm(): void {
    this.journalEntryForm = this.fb.group({
      EntryDate: ['', Validators.required],
      Description: ['', [Validators.required, Validators.maxLength(255)]],
      CreatedBy: ['', Validators.required]
    });
  }

  // Load users for the "CreatedBy" dropdown
  private loadUsers(): void {
    this.http.get<any[]>(`${this.apiUrl}/User`).subscribe({
      next: data => this.users = data,
      error: err => this.handleError('Failed to load users', err)
    });
  }
  // Method to map user ID to username
getCreatedByName(createdById: number): string {
  const user = this.users.find(u => u.UserId === createdById); // Match the user by userID
  return user ? user.Username : 'Unknown User'; // Return the username or 'Unknown User' if not found
}


  // Load existing journal entries to display
  private loadJournalEntries(): void {
    this.isLoading = true;
    this.http.get<any[]>(`${this.apiUrl}/JournalEntry`).subscribe({
      next: data => {
        this.journalEntries = data;
        this.isLoading = false;
      },
      error: err => {
        this.isLoading = false;
        this.handleError('Failed to load journal entries', err);
      }
    });
  }

  // Handle form submission
  onSubmit(): void {
    if (this.journalEntryForm.invalid) return;

    const entryData = this.journalEntryForm.value;
    this.http.post(`${this.apiUrl}/JournalEntry`, entryData).subscribe({
      next: () => {
        this.loadJournalEntries();
        this.journalEntryForm.reset();
        this.errorMessage = null;
      },
      error: err => this.handleError('Failed to add journal entry', err)
    });
  }

  // Handle errors
  private handleError(message: string, error: any): void {
    console.error(message, error);
    this.errorMessage = `${message}: ${error?.error?.title || 'Please try again later.'}`;
  }
}
