import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserRegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { IncomeStatementComponent } from './income-statement/income-statement.component';
import { BalanceSheetComponent } from './balance-sheet/balance-sheet.component';
import { JournalComponent } from './journal/journal.component';
import { LedgerComponent } from './ledger/ledger.component';
import { TrialBalanceComponent } from './trial-balance/trial-balance.component';
import { AccountsComponent } from './accounts/accounts.component';
import { JournalEntryDetailComponent } from './journal-entry-detail/journal-entry-detail.component';



const routes: Routes = [
  { path: 'register', component: UserRegistrationComponent },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'income-statement', component: IncomeStatementComponent },
  { path: 'balance-sheet', component: BalanceSheetComponent },
  { path: 'journal', component: JournalComponent },
  { path: 'ledger', component: LedgerComponent },
  { path: 'trial-balance', component: TrialBalanceComponent },
  {path: 'accounts' , component: AccountsComponent},
  {path: 'journal-entry-detail' , component: JournalEntryDetailComponent},
  
  
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' } // wildcard route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
