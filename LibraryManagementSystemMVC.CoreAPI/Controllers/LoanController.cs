using coreData.Data;
using coreModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemMVC.CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public LoanController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            if (_applicationDbContext.Loan == null)
            {
                return NotFound();
            }
            return await _applicationDbContext.Loan.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoans(int id)
        {
            if (_applicationDbContext.Loan == null)
            {
                return NotFound();
            }
            var loan = await _applicationDbContext.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            return loan;
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(Loan loan)
        {

            _applicationDbContext.Loan.Add(loan);
            await _applicationDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLoans), new { id = loan.LoanId }, loan);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutLoan(int id, Loan loan)
        {
            if (id != loan.LoanId)
            {
                return BadRequest();
            }
            _applicationDbContext.Entry(loan).State = EntityState.Modified;
            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLoan(int id)
        {
            if (_applicationDbContext.Loan == null)
            {
                return NotFound();
            }
            var loan = await _applicationDbContext.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            _applicationDbContext.Loan.Remove(loan);
            await _applicationDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
