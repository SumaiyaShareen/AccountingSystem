import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';

interface TrialBalanceEntry {
  AccountName: string;
  DebitTotal: number;
  CreditTotal: number;
}

@Component({
  selector: 'app-trial-balance',
  templateUrl: './trial-balance.component.html',
  styleUrls: ['./trial-balance.component.css']
})
export class TrialBalanceComponent implements OnInit {
  displayedColumns: string[] = ['AccountName', 'DebitTotal', 'CreditTotal'];
  dataSource = new MatTableDataSource<TrialBalanceEntry>();
  totalDebit: number = 0;
  totalCredit: number = 0;

  private readonly apiUrl = 'https://localhost:7159/api';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadTrialBalanceData();
  }

  private loadTrialBalanceData(): void {
    this.http.get<any[]>(`${this.apiUrl}/Accounts`).subscribe(accounts => {
      this.http.get<any[]>(`${this.apiUrl}/JournalEntryDetail`).subscribe(details => {
        
        const trialBalanceEntries = accounts.map(account => {
          const accountDetails = details.filter(detail => detail.AccountID === account.AccountID);
          
          const debitTotal = accountDetails.reduce((sum, detail) => sum + detail.DebitAmount, 0);
          const creditTotal = accountDetails.reduce((sum, detail) => sum + detail.CreditAmount, 0);
          
          this.totalDebit += debitTotal;
          this.totalCredit += creditTotal;

          return {
            AccountName: account.AccountName,
            DebitTotal: debitTotal,
            CreditTotal: creditTotal
          };
        });
        
        this.dataSource.data = trialBalanceEntries;
      });
    });
  }
}
