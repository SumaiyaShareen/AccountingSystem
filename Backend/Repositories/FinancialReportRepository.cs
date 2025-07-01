using Accounting_APIS.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting_APIS.Repositories
{
    public class FinancialReportRepository : IFinancialReportRepository
    {
        private readonly AccountingContext _context;

        public FinancialReportRepository(AccountingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FinancialReport>> GetAllFinancialReportsAsync()
        {
            return await _context.FinancialReports.ToListAsync();
        }

        public async Task<FinancialReport> GetFinancialReportByIdAsync(int reportId)
        {
            return await _context.FinancialReports.FindAsync(reportId);
        }

        public async Task AddFinancialReportAsync(FinancialReport report)
        {
            await _context.FinancialReports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFinancialReportAsync(FinancialReport report)
        {
            _context.FinancialReports.Update(report);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFinancialReportAsync(int reportId)
        {
            var report = await GetFinancialReportByIdAsync(reportId);
            if (report != null)
            {
                _context.FinancialReports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }
    }
}
