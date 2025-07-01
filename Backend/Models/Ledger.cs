using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Accounting_APIS.Models;

public partial class Ledger
{
    [Key]
    public int LedgerId { get; set; }

    public int AccountId { get; set; }

    public DateTime TransactionDate { get; set; }

    public decimal? DebitAmount { get; set; }

    public decimal? CreditAmount { get; set; }

    public decimal RunningBalance { get; set; }
    [JsonIgnore]
    public virtual Account? Account { get; set; } 
}
