import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-income-statement',
  templateUrl: './income-statement.component.html',
  styleUrls: ['./income-statement.component.css']
})
export class IncomeStatementComponent implements OnInit {
  revenueAccounts: any[] = [];
  expenseAccounts: any[] = [];
  totalRevenue: number = 0;
  totalExpense: number = 0;
  netIncome: number = 0;
  currentDate: string = '';  // Add currentDate property

  private readonly apiUrl = 'https://localhost:7159/api/Accounts'; // Make sure this API URL is correct

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadIncomeStatement();
    this.currentDate = new Date().toLocaleDateString();  // Set current date in the required format
  }

  loadIncomeStatement(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (data) => {
        // Filter Revenue and Expense accounts
        this.revenueAccounts = data.filter(account => account.AccountType === 'Revenue');
        this.expenseAccounts = data.filter(account => account.AccountType === 'Expense');

        // Calculate Total Revenue and Total Expenses
        this.totalRevenue = this.revenueAccounts.reduce((sum, account) => sum + account.OpeningBalance, 0);
        this.totalExpense = this.expenseAccounts.reduce((sum, account) => sum + account.OpeningBalance, 0);

        // Calculate Net Income (Revenue - Expenses)
        this.netIncome = this.totalRevenue - this.totalExpense;
      },
      (error) => {
        console.error('Error fetching income statement data:', error);
      }
    );
  }
}
