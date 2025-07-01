using Accounting_APIS.Models;
using Accounting_APIS.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialReportsController : ControllerBase
    {
        private readonly IFinancialReportRepository _financialReportRepository;

        public FinancialReportsController(IFinancialReportRepository financialReportRepository)
        {
            _financialReportRepository = financialReportRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialReport>>> GetAllFinancialReports()
        {
            var reports = await _financialReportRepository.GetAllFinancialReportsAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialReport>> GetFinancialReportById(int id)
        {
            var report = await _financialReportRepository.GetFinancialReportByIdAsync(id);
            if (report == null) return NotFound();
            return Ok(report);
        }

        [HttpPost]
        public async Task<ActionResult<FinancialReport>> CreateFinancialReport([FromBody] FinancialReport report)
        {
            await _financialReportRepository.AddFinancialReportAsync(report);
            return CreatedAtAction(nameof(GetFinancialReportById), new { id = report.ReportId }, report);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFinancialReport(int id, [FromBody] FinancialReport report)
        {
            if (id != report.ReportId) return BadRequest("Report ID mismatch");
            await _financialReportRepository.UpdateFinancialReportAsync(report);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFinancialReport(int id)
        {
            await _financialReportRepository.DeleteFinancialReportAsync(id);
            return NoContent();
        }
    }
}
