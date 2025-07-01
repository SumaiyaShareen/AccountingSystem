using Accounting_APIS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Repositories
{
    public interface ITrialBalanceRepository
    {
        Task<IEnumerable<TrialBalance>> GetAllTrialBalancesAsync();
        Task<TrialBalance> GetTrialBalanceByIdAsync(int trialBalanceId);
        Task AddTrialBalanceAsync(TrialBalance trialBalance);
        Task UpdateTrialBalanceAsync(TrialBalance trialBalance);
        Task DeleteTrialBalanceAsync(int trialBalanceId);
    }
}
