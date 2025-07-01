using Accounting_APIS.Models;
using Accounting_APIS.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportRequestsController : ControllerBase
    {
        private readonly IReportRequestRepository _reportRequestRepository;

        public ReportRequestsController(IReportRequestRepository reportRequestRepository)
        {
            _reportRequestRepository = reportRequestRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportRequest>>> GetAllReportRequests()
        {
            var requests = await _reportRequestRepository.GetAllReportRequestsAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportRequest>> GetReportRequestById(int id)
        {
            var request = await _reportRequestRepository.GetReportRequestByIdAsync(id);
            if (request == null) return NotFound();
            return Ok(request);
        }

        [HttpPost]
        public async Task<ActionResult<ReportRequest>> CreateReportRequest([FromBody] ReportRequest request)
        {
            await _reportRequestRepository.AddReportRequestAsync(request);
            return CreatedAtAction(nameof(GetReportRequestById), new { id = request.RequestId }, request);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReportRequest(int id, [FromBody] ReportRequest request)
        {
            if (id != request.RequestId) return BadRequest("Request ID mismatch");
            await _reportRequestRepository.UpdateReportRequestAsync(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReportRequest(int id)
        {
            await _reportRequestRepository.DeleteReportRequestAsync(id);
            return NoContent();
        }
    }
}
