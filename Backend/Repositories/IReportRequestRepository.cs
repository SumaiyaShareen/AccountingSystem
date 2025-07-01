using Accounting_APIS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Repositories
{
    public interface IReportRequestRepository
    {
        Task<IEnumerable<ReportRequest>> GetAllReportRequestsAsync();
        Task<ReportRequest> GetReportRequestByIdAsync(int requestId);
        Task AddReportRequestAsync(ReportRequest request);
        Task UpdateReportRequestAsync(ReportRequest request);
        Task DeleteReportRequestAsync(int requestId);
    }
}
