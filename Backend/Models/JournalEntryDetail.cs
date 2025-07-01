using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Accounting_APIS.Models;

public partial class JournalEntryDetail
{
    [Key]
    public int DetailId { get; set; }
    [Required]
    public int EntryId { get; set; }
    [Required]
    public int AccountId { get; set; }

    public decimal? DebitAmount { get; set; }

    public decimal? CreditAmount { get; set; }


    // This is where the relationship is established
    // This should be included in the model
    // This should be included in the model[[



    [JsonIgnore]
   public virtual Account? Account { get; set; }
    [JsonIgnore]
    public virtual JournalEntry? Entry { get; set; } 
}
