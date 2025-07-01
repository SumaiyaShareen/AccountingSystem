using Accounting_APIS.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting_APIS.Repositories
{
    public class JournalEntryRepository : IJournalEntryRepository
    {
        private readonly AccountingContext _context;

        public JournalEntryRepository(AccountingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JournalEntry>> GetAllJournalEntriesAsync()
        {
            return await _context.JournalEntries.ToListAsync();
        }

        public async Task<JournalEntry> GetJournalEntryByIdAsync(int entryId)
        {
            return await _context.JournalEntries.FindAsync(entryId);
        }

        public async Task AddJournalEntryAsync(JournalEntry journalEntry)
        {
            await _context.JournalEntries.AddAsync(journalEntry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateJournalEntryAsync(JournalEntry journalEntry)
        {
            _context.JournalEntries.Update(journalEntry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteJournalEntryAsync(int entryId)
        {
            var journalEntry = await GetJournalEntryByIdAsync(entryId);
            if (journalEntry != null)
            {
                _context.JournalEntries.Remove(journalEntry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
