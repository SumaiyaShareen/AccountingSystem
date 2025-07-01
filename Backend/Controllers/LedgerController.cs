using Accounting_APIS.Models;
using Accounting_APIS.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgerController : ControllerBase
    {
        private readonly ILedgerRepository _ledgerRepository;

        public LedgerController(ILedgerRepository ledgerRepository)
        {
            _ledgerRepository = ledgerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ledger>>> GetAllLedgers()
        {
            var ledgers = await _ledgerRepository.GetAllLedgersAsync();
            return Ok(ledgers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ledger>> GetLedgerById(int id)
        {
            var ledger = await _ledgerRepository.GetLedgerByIdAsync(id);
            if (ledger == null) return NotFound();
            return Ok(ledger);
        }

        [HttpPost]
        public async Task<ActionResult<Ledger>> CreateLedger([FromBody] Ledger ledger)
        {
            await _ledgerRepository.AddLedgerAsync(ledger);
            return CreatedAtAction(nameof(GetLedgerById), new { id = ledger.LedgerId }, ledger);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLedger(int id, [FromBody] Ledger ledger)
        {
            if (id != ledger.LedgerId) return BadRequest("Ledger ID mismatch");
            await _ledgerRepository.UpdateLedgerAsync(ledger);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLedger(int id)
        {
            await _ledgerRepository.DeleteLedgerAsync(id);
            return NoContent();
        }
    }
}
