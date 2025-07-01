using Accounting_APIS.Models;
using Accounting_APIS.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalEntryDetailController : ControllerBase
    {
        private readonly IJournalEntryDetailRepository _journalEntryDetailRepository;

        public JournalEntryDetailController(IJournalEntryDetailRepository journalEntryDetailRepository)
        {
            _journalEntryDetailRepository = journalEntryDetailRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JournalEntryDetail>>> GetAllJournalEntryDetails()
        {
            var details = await _journalEntryDetailRepository.GetAllJournalEntryDetailsAsync();
            return Ok(details);
        }


       

        [HttpGet("{id}")]
        public async Task<ActionResult<JournalEntryDetail>> GetJournalEntryDetailById(int id)
        {
            var detail = await _journalEntryDetailRepository.GetJournalEntryDetailByIdAsync(id);
            if (detail == null) return NotFound();
            return Ok(detail);
        }
        [HttpPost]
        public async Task<IActionResult> AddJournalEntryDetail([FromBody] JournalEntryDetail journalEntryDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Add the new journal entry detail
                await _journalEntryDetailRepository.AddJournalEntryDetailAsync(journalEntryDetail);
                

                return Ok(new { message = "Journal entry detail added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateJournalEntryDetail(int id, [FromBody] JournalEntryDetail detail)
        {
            if (id != detail.DetailId) return BadRequest("Detail ID mismatch");
            await _journalEntryDetailRepository.UpdateJournalEntryDetailAsync(detail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJournalEntryDetail(int id)
        {
            await _journalEntryDetailRepository.DeleteJournalEntryDetailAsync(id);
            return NoContent();
        }
    }
}
