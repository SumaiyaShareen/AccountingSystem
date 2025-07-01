using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Accounting_APIS.Models;

public partial class FinancialReport
{
    [Key]
    public int ReportId { get; set; }

    public string ReportType { get; set; } = null!;

    public DateTime ReportDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int GeneratedBy { get; set; }
    [JsonIgnore]
    public virtual User? GeneratedByNavigation { get; set; } 
}
