using coreData.Data;
using coreModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemMVC.CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PublisherController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            if (_applicationDbContext.Publishers == null)
            {
                return NotFound();
            }
            return await _applicationDbContext.Publishers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublishers(int id)
        {
            if (_applicationDbContext.Publishers == null)
            {
                return NotFound();
            }
            var publisher = await _applicationDbContext.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }

        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
        {

            _applicationDbContext.Publishers.Add(publisher);
            await _applicationDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPublishers), new { id = publisher.PublisherId }, publisher);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutPublisher(int id, Publisher publisher)
        {
            if (id != publisher.PublisherId)
            {
                return BadRequest();
            }
            _applicationDbContext.Entry(publisher).State = EntityState.Modified;
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
        public async Task<ActionResult> DeletePublisher(int id)
        {
            if (_applicationDbContext.Publishers == null)
            {
                return NotFound();
            }
            var publisher = await _applicationDbContext.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            _applicationDbContext.Publishers.Remove(publisher);
            await _applicationDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
