using Accounting_APIS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Repositories
{
    public class JournalEntryDetailRepository : IJournalEntryDetailRepository
    {
        private readonly AccountingContext _context;

        public JournalEntryDetailRepository(AccountingContext context)
        {
            _context = context;
        }

        // Get all Journal Entry Details
        public async Task<IEnumerable<JournalEntryDetail>> GetAllJournalEntryDetailsAsync()
        {
            return await _context.JournalEntryDetails
                .Include(jd => jd.Entry)   // Include related JournalEntry to get EntryDate and Description
                .Include(jd => jd.Account) // Include related Account to get Account details
                .ToListAsync();
        }

        // Get a specific Journal Entry Detail by its ID
        public async Task<JournalEntryDetail> GetJournalEntryDetailByIdAsync(int detailId)
        {
            return await _context.JournalEntryDetails
                .Include(jd => jd.Entry)   // Include related JournalEntry to get EntryDate and Description
                .Include(jd => jd.Account) // Include related Account to get Account details
                .FirstOrDefaultAsync(jd => jd.DetailId == detailId);
        }

        // Add a new Journal Entry Detail
        public async Task AddJournalEntryDetailAsync(JournalEntryDetail detail)
        {
            await _context.JournalEntryDetails.AddAsync(detail);
            await _context.SaveChangesAsync();
        }

        // Update an existing Journal Entry Detail
        public async Task UpdateJournalEntryDetailAsync(JournalEntryDetail detail)
        {
            var existingDetail = await _context.JournalEntryDetails
                .Include(jd => jd.Entry)  // Include to check if related entities exist
                .Include(jd => jd.Account)
                .FirstOrDefaultAsync(jd => jd.DetailId == detail.DetailId);

            if (existingDetail != null)
            {
                // Update properties based on the passed detail object
                existingDetail.DebitAmount = detail.DebitAmount;
                existingDetail.CreditAmount = detail.CreditAmount;

                // You might need to set these based on the related entities
                existingDetail.Entry = detail.Entry;    // Update the entire Entry object if necessary
                existingDetail.Account = detail.Account; // Update the entire Account object if necessary

                _context.JournalEntryDetails.Update(existingDetail);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a Journal Entry Detail by its ID
        public async Task DeleteJournalEntryDetailAsync(int detailId)
        {
            var detail = await _context.JournalEntryDetails.FindAsync(detailId);
            if (detail != null)
            {
                _context.JournalEntryDetails.Remove(detail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
