using Accounting_APIS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Repositories
{
    public interface IJournalEntryRepository
    {
        Task<IEnumerable<JournalEntry>> GetAllJournalEntriesAsync();
        Task<JournalEntry> GetJournalEntryByIdAsync(int entryId);
        Task AddJournalEntryAsync(JournalEntry journalEntry);
        Task UpdateJournalEntryAsync(JournalEntry journalEntry);
        Task DeleteJournalEntryAsync(int entryId);
    }
}
