import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface LedgerEntry {
  AccountName: string;
  OpeningBalance: number;
  DebitAmount: number;
  CreditAmount: number;
  TransactionDate: string;
  RunningBalance: number;
}

@Component({
  selector: 'app-ledger',
  templateUrl: './ledger.component.html',
  styleUrls: ['./ledger.component.css']
})
export class LedgerComponent implements OnInit {
  ledgerData: LedgerEntry[] = []; // Final combined ledger data
  accounts: any[] = [];
  journalDetails: any[] = [];
  journalEntries: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchLedgerData();
  }

  fetchLedgerData(): void {
    const accountsApiUrl = 'https://localhost:7159/api/Accounts';
    const journalDetailsApiUrl = 'https://localhost:7159/api/JournalEntryDetail';
    const journalEntriesApiUrl = 'https://localhost:7159/api/JournalEntry';

    // Fetch all data in parallel
    Promise.all([
      this.http.get<any[]>(accountsApiUrl).toPromise(),
      this.http.get<any[]>(journalDetailsApiUrl).toPromise(),
      this.http.get<any[]>(journalEntriesApiUrl).toPromise()
    ])
      .then(([accounts, journalDetails, journalEntries]) => {
        this.accounts = accounts || [];
        this.journalDetails = journalDetails || [];
        this.journalEntries = journalEntries || [];

        // Combine data
        this.ledgerData = this.combineLedgerData();
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
  }

  combineLedgerData(): LedgerEntry[] {
    const ledger: LedgerEntry[] = []; // Explicitly typed array

    // Loop through journal details
    this.journalDetails.forEach(detail => {
      const account = this.accounts.find(acc => acc.AccountId=== detail.AccountId);
      const journalEntry = this.journalEntries.find(je => je.EntryId === detail.EntryId);

      if (account && journalEntry) {
        ledger.push({
          AccountName: account.AccountName,
          OpeningBalance: account.OpeningBalance,
          DebitAmount: detail.DebitAmount,
          CreditAmount: detail.CreditAmount,
          TransactionDate: journalEntry.EntryDate,
          RunningBalance: 0 // Placeholder; calculate below
        });
      }
    });

    // Calculate Running Balance
    ledger.sort((a, b) => new Date(a.TransactionDate).getTime() - new Date(b.TransactionDate).getTime());

    const balances = new Map<string, number>(); // Store running balances per account
    ledger.forEach(entry => {
      const prevBalance = balances.get(entry.AccountName) || entry.OpeningBalance;
      const newBalance = prevBalance + (entry.DebitAmount - entry.CreditAmount);
      entry.RunningBalance = newBalance;
      balances.set(entry.AccountName, newBalance);
    });

    return ledger;
  }
}
