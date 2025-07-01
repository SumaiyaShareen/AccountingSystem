using Accounting_APIS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountByIdAsync(int accountId);
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int accountId);
    }
}
