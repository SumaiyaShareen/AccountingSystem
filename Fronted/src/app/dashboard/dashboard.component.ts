import { Component } from '@angular/core';

interface ReportLink {
  label: string;
  path: string;
}

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  // Define links to the various reports
  reportLinks: ReportLink[] = [
    { label: 'Income Statement', path: '/income-statement' },
    { label: 'Balance Sheet', path: '/balance-sheet' },
    { label: 'Journal', path: '/journal' },
    { label: 'Ledger', path: '/ledger' },
    { label: 'Trial Balance', path: '/trial-balance' },
    {label: 'Accounts' , path: '/accounts'},
    {label: 'JournalEntryDetail' , path: '/journal-entry-detail'},
    
    
  ];

  constructor() {}
}
