CREATE DATABASE ACCOUNTING;

USE ACCOUNTING;

-- 1. Users Table: Store system users
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(20) CHECK (Role IN ('Admin', 'Accountant', 'User')) NOT NULL
);
select * from Users
-- 2. Accounts Table: Store chart of accounts
CREATE TABLE Accounts (
    AccountID INT IDENTITY(1,1) PRIMARY KEY,
    AccountName VARCHAR(100) NOT NULL,
    AccountType VARCHAR(20) CHECK (AccountType IN ('Asset', 'Liability', 'Equity', 'Revenue', 'Expense')) NOT NULL,
    OpeningBalance DECIMAL(18, 2) DEFAULT 0.00
);

-- 3. JournalEntries Table: Store main journal entries
CREATE TABLE JournalEntries (
    EntryID INT IDENTITY(1,1) PRIMARY KEY,
    EntryDate DATE NOT NULL,
    Description VARCHAR(255) NOT NULL,
    CreatedBy INT NOT NULL,
    CONSTRAINT FK_JournalEntries_Users FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
);

select * from JournalEntries;



DELETE FROM JournalEntries
WHERE EntryID = 8












-- 4. JournalEntryDetails Table: Store details of each journal entry
CREATE TABLE JournalEntryDetails (
    DetailID INT IDENTITY(1,1) PRIMARY KEY,
    EntryID INT NOT NULL,
    AccountID INT NOT NULL,
    DebitAmount DECIMAL(18, 2) DEFAULT 0.00,
    CreditAmount DECIMAL(18, 2) DEFAULT 0.00,
    CONSTRAINT FK_JournalEntryDetails_JournalEntries FOREIGN KEY (EntryID) REFERENCES JournalEntries(EntryID),
    CONSTRAINT FK_JournalEntryDetails_Accounts FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
);
select * from JournalEntryDetails;

SELECT 
    jd.DetailID,
    jd.DebitAmount,
    jd.CreditAmount,
    je.EntryDate,
    je.Description
FROM 
    JournalEntryDetails jd
JOIN 
    JournalEntries je ON jd.EntryID = je.EntryID

	SELECT EntryID, EntryDate, Description FROM JournalEntries;













SELECT 
    JED.DetailID, 
    JE.EntryID, 
    JE.EntryDate, 
    JE.Description, 
    JED.AccountID, 
    JED.DebitAmount, 
    JED.CreditAmount
FROM 
    JournalEntryDetails JED
JOIN 
    JournalEntries JE ON JED.EntryID = JE.EntryID;


-- 5. Ledger Table: Store ledger transactions with running balances
CREATE TABLE Ledger (
    LedgerID INT IDENTITY(1,1) PRIMARY KEY,
    AccountID INT NOT NULL,
    TransactionDate DATE NOT NULL,
    DebitAmount DECIMAL(18, 2) DEFAULT 0.00,
    CreditAmount DECIMAL(18, 2) DEFAULT 0.00,
    RunningBalance DECIMAL(18, 2) NOT NULL,
    CONSTRAINT FK_Ledger_Accounts FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
);

-- 6. TrialBalance Table: Store trial balance summaries
CREATE TABLE TrialBalance (
    TrialBalanceID INT IDENTITY(1,1) PRIMARY KEY,
    AccountID INT NOT NULL,
    DebitTotal DECIMAL(18, 2) DEFAULT 0.00,
    CreditTotal DECIMAL(18, 2) DEFAULT 0.00,
    CONSTRAINT FK_TrialBalance_Accounts FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
);

-- 7. FinancialReports Table: Store income statements and balance sheets
CREATE TABLE FinancialReports (
    ReportID INT IDENTITY(1,1) PRIMARY KEY,
    ReportType VARCHAR(20) CHECK (ReportType IN ('Income Statement', 'Balance Sheet')) NOT NULL,
    ReportDate DATE NOT NULL,
    TotalAmount DECIMAL(18, 2) NOT NULL,
    GeneratedBy INT NOT NULL,
    CONSTRAINT FK_FinancialReports_Users FOREIGN KEY (GeneratedBy) REFERENCES Users(UserID)
);

-- 8. ReportRequests Table: Store user requests for reports
CREATE TABLE ReportRequests (
    RequestID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    ReportType VARCHAR(20) CHECK (ReportType IN ('Journal', 'Ledger', 'Trial Balance', 'Income Statement', 'Balance Sheet')) NOT NULL,
    RequestDate DATE DEFAULT GETDATE(),
    Status VARCHAR(20) DEFAULT 'Pending',
    CONSTRAINT FK_ReportRequests_Users FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
