using Accounting_APIS.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting_APIS.Repositories
{
    public class TrialBalanceRepository : ITrialBalanceRepository
    {
        private readonly AccountingContext _context;

        public TrialBalanceRepository(AccountingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrialBalance>> GetAllTrialBalancesAsync()
        {
            return await _context.TrialBalances.ToListAsync();
        }

        public async Task<TrialBalance> GetTrialBalanceByIdAsync(int trialBalanceId)
        {
            return await _context.TrialBalances.FindAsync(trialBalanceId);
        }

        public async Task AddTrialBalanceAsync(TrialBalance trialBalance)
        {
            await _context.TrialBalances.AddAsync(trialBalance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTrialBalanceAsync(TrialBalance trialBalance)
        {
            _context.TrialBalances.Update(trialBalance);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTrialBalanceAsync(int trialBalanceId)
        {
            var trialBalance = await GetTrialBalanceByIdAsync(trialBalanceId);
            if (trialBalance != null)
            {
                _context.TrialBalances.Remove(trialBalance);
                await _context.SaveChangesAsync();
            }
        }
    }
}
