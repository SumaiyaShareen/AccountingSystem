using Accounting_APIS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Repositories
{
    public interface ILedgerRepository
    {
        Task<IEnumerable<Ledger>> GetAllLedgersAsync();
        Task<Ledger> GetLedgerByIdAsync(int ledgerId);
        Task AddLedgerAsync(Ledger ledger);
        Task UpdateLedgerAsync(Ledger ledger);
        Task DeleteLedgerAsync(int ledgerId);
    }
}
