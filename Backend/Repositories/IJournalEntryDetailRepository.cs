using Accounting_APIS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Repositories
{
    public interface IJournalEntryDetailRepository
    {
        Task<IEnumerable<JournalEntryDetail>> GetAllJournalEntryDetailsAsync();

       // Task<IEnumerable<JournalEntryDetail>> GetJournalEntryDetailsWithEntriesAsync();
        Task<JournalEntryDetail> GetJournalEntryDetailByIdAsync(int detailId);
        Task AddJournalEntryDetailAsync(JournalEntryDetail detail);


        Task UpdateJournalEntryDetailAsync(JournalEntryDetail detail);
        Task DeleteJournalEntryDetailAsync(int detailId);
    }
}
