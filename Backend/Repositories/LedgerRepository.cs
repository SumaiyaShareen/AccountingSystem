using Accounting_APIS.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting_APIS.Repositories
{
    public class LedgerRepository : ILedgerRepository
    {
        private readonly AccountingContext _context;

        public LedgerRepository(AccountingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ledger>> GetAllLedgersAsync()
        {
            return await _context.Ledgers.ToListAsync();
        }

        public async Task<Ledger> GetLedgerByIdAsync(int ledgerId)
        {
            return await _context.Ledgers.FindAsync(ledgerId);
        }

        public async Task AddLedgerAsync(Ledger ledger)
        {
            await _context.Ledgers.AddAsync(ledger);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLedgerAsync(Ledger ledger)
        {
            _context.Ledgers.Update(ledger);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLedgerAsync(int ledgerId)
        {
            var ledger = await GetLedgerByIdAsync(ledgerId);
            if (ledger != null)
            {
                _context.Ledgers.Remove(ledger);
                await _context.SaveChangesAsync();
            }
        }
    }
}
