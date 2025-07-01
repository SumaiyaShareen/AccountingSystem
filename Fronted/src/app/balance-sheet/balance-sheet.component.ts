import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-balance-sheet',
  templateUrl: './balance-sheet.component.html',
  styleUrls: ['./balance-sheet.component.css'],
})
export class BalanceSheetComponent implements OnInit {
  assets: any[] = [];
  liabilities: any[] = [];
  equity: any[] = [];
  totalAssets: number = 0;
  totalLiabilitiesAndEquity: number = 0;

  private readonly apiUrl = 'https://localhost:7159/api/Accounts';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadBalanceSheet();
  }

  loadBalanceSheet(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (data) => {
        // Filter accounts into assets, liabilities, and equity
        this.assets = data.filter((account) => account.AccountType === 'Asset');
        this.liabilities = data.filter((account) => account.AccountType === 'Liability');
        this.equity = data.filter((account) => account.AccountType === 'Equity');

        // Calculate totals
        this.totalAssets = this.assets.reduce((sum, item) => sum + item.balance, 0);
        this.totalLiabilitiesAndEquity =
          this.liabilities.reduce((sum, item) => sum + item.balance, 0) +
          this.equity.reduce((sum, item) => sum + item.balance, 0);
      },
      (error) => {
        console.error('Error fetching balance sheet data:', error);
      }
    );
  }
}
