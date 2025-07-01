import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-journal-entry-detail',
  templateUrl: './journal-entry-detail.component.html',
  styleUrls: ['./journal-entry-detail.component.css']
})
export class JournalEntryDetailComponent implements OnInit {
  journalDetailForm!: FormGroup;

  journalEntries: any[] = [];
  accounts: any[] = [];
  journalEntryDetails: any[] = [];
  combinedDataSource: MatTableDataSource<any> = new MatTableDataSource<any>();  // MatTableDataSource to handle table data
  isLoading = false;
  errorMessage: string | null = null;

  private readonly apiUrl = 'https://localhost:7159/api';

  constructor(private fb: FormBuilder, private http: HttpClient) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadJournalEntries();
    this.loadAccounts();
    this.loadJournalEntryDetails();
  }

  private initializeForm(): void {
    this.journalDetailForm = this.fb.group({
      EntryID: ['', Validators.required],
      AccountID: ['', Validators.required],
      DebitAmount: [{ value: 0.00, disabled: true }],
      CreditAmount: [{ value: 0.00, disabled: true }]
    });
  }

  private loadJournalEntries(): void {
    this.http.get<any[]>(`${this.apiUrl}/JournalEntry`).subscribe({
      next: data => {
        console.log("Loaded Journal Entries:", data);  // Log data to inspect
        this.journalEntries = data;
        this.combineData();  // Combine data after journal entries are loaded
      },
      error: err => this.handleError('Failed to load journal entries', err)
    });
  }

  private loadAccounts(): void {
    this.http.get<any[]>(`${this.apiUrl}/Accounts`).subscribe({
      next: data => {
        this.accounts = data;
        this.combineData();  // Combine data after accounts are loaded
      },
      error: err => this.handleError('Failed to load accounts', err)
    });
  }

  private loadJournalEntryDetails(): void {
    this.http.get<any[]>(`${this.apiUrl}/JournalEntryDetail`).subscribe({
      next: (data) => {
        this.journalEntryDetails = data;  // Store the data that includes EntryDate and Description
        this.combineData();  // Combine data after journal entry details are loaded
      },
      error: (err) => {
        console.error('Failed to load journal entry details', err);
      },
    });
  }

  onAccountChange(accountID: number): void {
    const selectedAccount = this.accounts.find(account => account.AccountId === accountID);
    if (selectedAccount) {
      if (selectedAccount.type === 'Asset' || selectedAccount.type === 'Expense') {
        this.journalDetailForm.get('DebitAmount')?.enable();
        this.journalDetailForm.get('CreditAmount')?.disable();
      } else {
        this.journalDetailForm.get('CreditAmount')?.enable();
        this.journalDetailForm.get('DebitAmount')?.disable();
      }
    }
  }

  onSubmit(): void {
    if (this.journalDetailForm.invalid) return;

    const detailData = this.journalDetailForm.value;
    this.http.post(`${this.apiUrl}/JournalEntryDetail`, detailData).subscribe({
      next: () => {
        this.loadJournalEntryDetails();
        this.journalDetailForm.reset();
        this.errorMessage = null;
      },
      error: err => this.handleError('Failed to add journal entry detail', err)
    });
  }

  // Combine data from JournalEntries, JournalEntryDetails, and Accounts
  combineData(): void {
    if (this.journalEntries.length > 0 && this.journalEntryDetails.length > 0 && this.accounts.length > 0) {
      const combinedData = this.journalEntries.map(entry => {
        return this.journalEntryDetails.filter(detail => detail.EntryId === entry.EntryId).map(detail => {
          const account = this.accounts.find(acc => acc.AccountId === detail.AccountId);
          return {
            EntryDate: entry.EntryDate,
            Description: entry.Description,
            AccountName: account ? account.AccountName : 'Unknown Account',
            DebitAmount: detail.DebitAmount,
            CreditAmount: detail.CreditAmount
          };
        });
      }).flat();  // Flatten the result into a single array

      this.combinedDataSource.data = combinedData;  // Assign combined data to MatTableDataSource
    }
  }

  private handleError(message: string, error: any): void {
    console.error(message, error);
    this.errorMessage = `${message}: ${error?.error?.title || 'Please try again later.'}`;
  }
}
