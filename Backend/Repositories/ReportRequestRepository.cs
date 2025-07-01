using Accounting_APIS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Repositories
{
    public class ReportRequestRepository : IReportRequestRepository
    {
        private readonly AccountingContext _context;

        public ReportRequestRepository(AccountingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReportRequest>> GetAllReportRequestsAsync()
        {
            return await _context.ReportRequests.ToListAsync();
        }

        public async Task<ReportRequest> GetReportRequestByIdAsync(int requestId)
        {
            return await _context.ReportRequests.FindAsync(requestId);
        }

        public async Task AddReportRequestAsync(ReportRequest request)
        {
            await _context.ReportRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReportRequestAsync(ReportRequest request)
        {
            _context.ReportRequests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReportRequestAsync(int requestId)
        {
            var request = await GetReportRequestByIdAsync(requestId);
            if (request != null)
            {
                _context.ReportRequests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }
    }
}
