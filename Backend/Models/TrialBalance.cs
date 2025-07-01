using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Accounting_APIS.Models;

public partial class TrialBalance
{
    [Key]
    public int TrialBalanceId { get; set; }
    [ForeignKey("Account")]
    public int AccountId { get; set; }

    public decimal? DebitTotal { get; set; }

    public decimal? CreditTotal { get; set; }

    [JsonIgnore]
    public virtual Account? Account { get; set; } 
}
