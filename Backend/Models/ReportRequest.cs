using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Accounting_APIS.Models;

public partial class ReportRequest
{
    [Key]
    public int RequestId { get; set; }

    public int UserId { get; set; }

    public string ReportType { get; set; } = null!;

    public DateTime RequestDate { get; set; }

    public string? Status { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; } 
}
