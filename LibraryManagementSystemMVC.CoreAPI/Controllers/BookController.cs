using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using coreModel;
using Microsoft.EntityFrameworkCore;
using coreData.Data;

namespace LibraryManagementSystemMVC.CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BookController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            if (_applicationDbContext.Books == null)
            {
                return NotFound();
            }
            return await _applicationDbContext.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooks(int id)
        {
            if (_applicationDbContext.Books == null)
            {
                return NotFound();
            }
            var book = await _applicationDbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {

            _applicationDbContext.Books.Add(book);
                await _applicationDbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBooks), new { id = book.BookId }, book);
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }
            _applicationDbContext.Entry(book).State = EntityState.Modified;
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
        public async Task<ActionResult> DeleteBook(int id)
        {
            if (_applicationDbContext.Books == null)
            {
                return NotFound();
            }
            var book = await _applicationDbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _applicationDbContext.Books.Remove(book);
            await _applicationDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
