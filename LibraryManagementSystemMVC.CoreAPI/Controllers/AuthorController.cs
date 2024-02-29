using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coreData;
using coreModel;
using coreData.Data;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryManagementSystemMVC.CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AuthorController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            if (_applicationDbContext.Authors == null)
            {
                return NotFound();
            }
            return await _applicationDbContext.Authors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthors(int id)
        {
            if (_applicationDbContext.Authors == null)
            {
                return NotFound();
            }
            var author = await _applicationDbContext.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            _applicationDbContext.Authors.Add(author);
            await _applicationDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthors), new { id = author.AuthorId }, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }
            _applicationDbContext.Entry(author).State = EntityState.Modified;
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
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            if (_applicationDbContext.Authors == null)
            {
                return NotFound();
            }
            var author = await _applicationDbContext.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            _applicationDbContext.Authors.Remove(author);
            await _applicationDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
