using Accounting_APIS.Models;
using Accounting_APIS.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting_APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrialBalanceController : ControllerBase
    {
        private readonly ITrialBalanceRepository _trialBalanceRepository;

        public TrialBalanceController(ITrialBalanceRepository trialBalanceRepository)
        {
            _trialBalanceRepository = trialBalanceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrialBalance>>> GetAllTrialBalances()
        {
            var trialBalances = await _trialBalanceRepository.GetAllTrialBalancesAsync();
            return Ok(trialBalances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrialBalance>> GetTrialBalanceById(int id)
        {
            var trialBalance = await _trialBalanceRepository.GetTrialBalanceByIdAsync(id);
            if (trialBalance == null) return NotFound();
            return Ok(trialBalance);
        }

        [HttpPost]
        public async Task<ActionResult<TrialBalance>> CreateTrialBalance([FromBody] TrialBalance trialBalance)
        {
            await _trialBalanceRepository.AddTrialBalanceAsync(trialBalance);
            return CreatedAtAction(nameof(GetTrialBalanceById), new { id = trialBalance.TrialBalanceId }, trialBalance);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTrialBalance(int id, [FromBody] TrialBalance trialBalance)
        {
            if (id != trialBalance.TrialBalanceId) return BadRequest("Trial Balance ID mismatch");
            await _trialBalanceRepository.UpdateTrialBalanceAsync(trialBalance);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrialBalance(int id)
        {
            await _trialBalanceRepository.DeleteTrialBalanceAsync(id);
            return NoContent();
        }
    }
}
