using Accounting_APIS.Models;
using Accounting_APIS.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalEntryController : ControllerBase
    {
        private readonly IJournalEntryRepository _journalEntryRepository;

        public JournalEntryController(IJournalEntryRepository journalEntryRepository)
        {
            _journalEntryRepository = journalEntryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JournalEntry>>> GetAllJournalEntries()
        {
            var entries = await _journalEntryRepository.GetAllJournalEntriesAsync();
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JournalEntry>> GetJournalEntryById(int id)
        {
            var entry = await _journalEntryRepository.GetJournalEntryByIdAsync(id);
            if (entry == null) return NotFound();
            return Ok(entry);
        }

        [HttpPost]
        public async Task<ActionResult<JournalEntry>> CreateJournalEntry([FromBody] JournalEntry journalEntry)
        {
            await _journalEntryRepository.AddJournalEntryAsync(journalEntry);
            return CreatedAtAction(nameof(GetJournalEntryById), new { id = journalEntry.EntryId }, journalEntry);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateJournalEntry(int id, [FromBody] JournalEntry journalEntry)
        {
            if (id != journalEntry.EntryId) return BadRequest("Entry ID mismatch");
            await _journalEntryRepository.UpdateJournalEntryAsync(journalEntry);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJournalEntry(int id)
        {
            await _journalEntryRepository.DeleteJournalEntryAsync(id);
            return NoContent();
        }
    }
}
