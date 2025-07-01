import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css']
})
export class AccountsComponent implements OnInit {
  accounts: any[] = [];
  accountForm: FormGroup;
  isEditing = false;
  selectedAccount: any = null;

  // API base URL
  private readonly apiUrl = 'https://localhost:7159/api/Accounts';

  constructor(private http: HttpClient, private fb: FormBuilder) {
    this.accountForm = this.fb.group({
      AccountName: ['', Validators.required],
      AccountType: ['', Validators.required],
      OpeningBalance: [0.00, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    this.loadAccounts();
  }

  // Load accounts from API
  loadAccounts(): void {
    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (data) => this.accounts = data,
      error: (err) => console.error('Error loading accounts:', err)
    });
  }

  // Add or update account based on the form state
  // Add or update account based on the form state
saveAccount(): void {
  const accountData = { ...this.accountForm.value, AccountId: this.selectedAccount?.AccountId }; // Ensure AccountId is included
  
  if (this.isEditing && this.selectedAccount) {
    // **UPDATE** existing account
    this.http.put(`${this.apiUrl}/${this.selectedAccount.AccountId}`, accountData).subscribe({
      next: () => {
        this.loadAccounts();  // Reload accounts list
        this.resetForm();
        alert('Account updated successfully');
      },
      error: (err) => {
        console.error('Error updating account:', err); // More detailed error logging
        alert('Failed to update the account');
      }
    });
  } else {
    // **ADD** new account
    this.http.post(this.apiUrl, accountData).subscribe({
      next: () => {
        this.loadAccounts();  // Reload accounts list
        this.resetForm();
        alert('Account added successfully');
      },
      error: (err) => {
        console.error('Error adding account:', err);
        alert('Failed to add the account');
      }
    });
  }
}


  // Select an account to edit
  editAccount(account: any): void {
    this.isEditing = true;
    this.selectedAccount = account;
    this.accountForm.patchValue(account);
  }

  // **DELETE** an account
  deleteItem(id: number): void {
    this.http.delete(`${this.apiUrl}/${id}`).pipe(
      catchError((error) => {
        console.error('Delete failed', error);
        return throwError(error);
      })
    ).subscribe({
      next: () => {
        this.loadAccounts();  // Reload accounts list after deletion
        alert('Account deleted successfully');
      },
      error: (err) => {
        console.error('Error deleting account:', err);
        alert('Failed to delete the account');
      }
    });
  }

  // Reset form and editing state
  resetForm(): void {
    this.accountForm.reset({ AccountType: '', OpeningBalance: 0.00 });
    this.isEditing = false;
    this.selectedAccount = null;
  }
}
