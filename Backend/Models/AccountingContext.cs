using System;
using Microsoft.EntityFrameworkCore;

namespace Accounting_APIS.Models
{
    public partial class AccountingContext : DbContext
    {
        public AccountingContext(DbContextOptions<AccountingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<FinancialReport> FinancialReports { get; set; }
        public virtual DbSet<JournalEntry> JournalEntries { get; set; }
        public virtual DbSet<JournalEntryDetail> JournalEntryDetails { get; set; }
        public virtual DbSet<Ledger> Ledgers { get; set; }
        public virtual DbSet<ReportRequest> ReportRequests { get; set; }
        public virtual DbSet<TrialBalance> TrialBalances { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA5862F3F983C");
                entity.Property(e => e.AccountId).HasColumnName("AccountID");
                entity.Property(e => e.AccountName).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.AccountType).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.OpeningBalance)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<FinancialReport>(entity =>
            {
                entity.HasKey(e => e.ReportId).HasName("PK__Financia__D5BD48E56F130DA6");
                entity.Property(e => e.ReportId).HasColumnName("ReportID");
                entity.Property(e => e.ReportType).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.GeneratedByNavigation)
                    .WithMany(p => p.FinancialReports)
                    .HasForeignKey(d => d.GeneratedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FinancialReports_Users");
            });

            modelBuilder.Entity<JournalEntry>(entity =>
            {
                entity.HasKey(e => e.EntryId).HasName("PK__JournalE__F57BD2D769B4A930");
                entity.Property(e => e.EntryId).HasColumnName("EntryID");
                entity.Property(e => e.Description).HasMaxLength(255).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.JournalEntries)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JournalEntries_Users");
            });

            modelBuilder.Entity<JournalEntryDetail>(entity =>
            {
                entity.HasKey(e => e.DetailId).HasName("PK__JournalE__135C314D10AE94A5");
                entity.Property(e => e.DetailId).HasColumnName("DetailID");
                entity.Property(e => e.AccountId).HasColumnName("AccountID");
                entity.Property(e => e.CreditAmount)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.DebitAmount)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.EntryId).HasColumnName("EntryID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.JournalEntryDetails)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JournalEntryDetails_Accounts");

                entity.HasOne(d => d.Entry)
                    .WithMany(p => p.JournalEntryDetails)
                    .HasForeignKey(d => d.EntryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JournalEntryDetails_JournalEntries");
            });

            modelBuilder.Entity<Ledger>(entity =>
            {
                entity.HasKey(e => e.LedgerId).HasName("PK__Ledger__AE70E0AF5F392F48");
                entity.ToTable("Ledger");
                entity.Property(e => e.LedgerId).HasColumnName("LedgerID");
                entity.Property(e => e.AccountId).HasColumnName("AccountID");
                entity.Property(e => e.CreditAmount)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.DebitAmount)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.RunningBalance).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Ledgers)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ledger_Accounts");
            });

            modelBuilder.Entity<ReportRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId).HasName("PK__ReportRe__33A8519A09693BFE");
                entity.Property(e => e.RequestId).HasColumnName("RequestID");
                entity.Property(e => e.ReportType).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.RequestDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValue("Pending");
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReportRequests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportRequests_Users");
            });

            modelBuilder.Entity<TrialBalance>(entity =>
            {
                entity.HasKey(e => e.TrialBalanceId).HasName("PK__TrialBal__E1FEAE15FBDBED82");
                entity.ToTable("TrialBalance");
                entity.Property(e => e.TrialBalanceId).HasColumnName("TrialBalanceID");
                entity.Property(e => e.AccountId).HasColumnName("AccountID");
                entity.Property(e => e.CreditTotal)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.DebitTotal)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TrialBalances)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrialBalance_Accounts");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC5FFEA2E3");
                entity.HasIndex(e => e.Username, "UQ__Users__536C85E4BA7CBE4C").IsUnique();
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.PasswordHash).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.Role).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Username).HasMaxLength(50).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
