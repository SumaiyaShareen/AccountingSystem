using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Accounting_APIS.Models;

public partial class Account
{
    [Key]
   
    public int AccountId { get; set; }

    public string AccountName { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public decimal? OpeningBalance { get; set; }
    [JsonIgnore]
    public virtual ICollection<JournalEntryDetail> JournalEntryDetails { get; set; } = new List<JournalEntryDetail>();
    [JsonIgnore]
    public virtual ICollection<Ledger> Ledgers { get; set; } = new List<Ledger>();
    [JsonIgnore]
    public virtual ICollection<TrialBalance> TrialBalances { get; set; } = new List<TrialBalance>();
    
}
