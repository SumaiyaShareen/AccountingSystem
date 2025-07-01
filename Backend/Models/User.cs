using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Accounting_APIS.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;


    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }










    [JsonIgnore]
    public virtual ICollection<FinancialReport> FinancialReports { get; set; } = new List<FinancialReport>();
    [JsonIgnore]
    public virtual ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    [JsonIgnore]
  
    public virtual ICollection<ReportRequest> ReportRequests { get; set; } = new List<ReportRequest>();
}
