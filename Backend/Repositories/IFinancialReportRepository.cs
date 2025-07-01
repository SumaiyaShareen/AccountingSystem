using Accounting_APIS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Repositories
{
    public interface IFinancialReportRepository
    {
        Task<IEnumerable<FinancialReport>> GetAllFinancialReportsAsync();
        Task<FinancialReport> GetFinancialReportByIdAsync(int reportId);
        Task AddFinancialReportAsync(FinancialReport report);
        Task UpdateFinancialReportAsync(FinancialReport report);
        Task DeleteFinancialReportAsync(int reportId);
    }
}
