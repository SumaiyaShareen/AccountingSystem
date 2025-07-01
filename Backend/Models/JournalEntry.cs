using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Accounting_APIS.Models;

public partial class JournalEntry
{
    [Key]
    public int EntryId { get; set; }

    public DateTime EntryDate { get; set; }

    public string Description { get; set; } = null!;

    public int CreatedBy { get; set; }
    [JsonIgnore]
    public virtual User? CreatedByNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<JournalEntryDetail> JournalEntryDetails { get; set; } = new List<JournalEntryDetail>();
}
