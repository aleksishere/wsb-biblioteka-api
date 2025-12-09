using BibliotekaAPI.Data;
using BibliotekaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaAPI.Controllers
{
    [Route("copies")]
    [ApiController]
    public class CopiesController : ControllerBase
    {
        private readonly Library _context;

        public CopiesController(Library context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Copy>>> GetCopies()
        {
            return await _context.Copies.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Copy>> PostCopy(Copy copy)
        {
            if (!await _context.Books.AnyAsync(b => b.Id == copy.BookId))
                return BadRequest("Niepoprawne id ksiązki");

            _context.Copies.Add(copy);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCopies), new { id = copy.Id }, copy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCopy(int id)
        {
            var copy = await _context.Copies.FindAsync(id);
            if (copy == null) return NotFound();

            _context.Copies.Remove(copy);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}