using BibliotekaAPI.Data;
using BibliotekaAPI.DTOs;
using BibliotekaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaAPI.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Library _context;

        public BooksController(Library context)
        {
            _context = context;
        }

        // GET: /books?authorId
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Books>>> GetBooks([FromQuery] long? authorId)
        {
            var query = _context.Books.Include(b => b.Author).AsQueryable();

            if (authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> GetBook(long id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Books>> PostBook(CreateBook dto)
        {
            var author = await _context.Authors.FindAsync(dto.AuthorId);
            if (author == null)
            {
                return BadRequest("Niepoprawne id autora");
            }

            var book = new Books
            {
                Title = dto.Title,
                Year = dto.Year,
                AuthorId = dto.AuthorId,
                Author = author
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(long id, BookUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();
            
            if (!await _context.Authors.AnyAsync(a => a.Id == dto.AuthorId))
                return BadRequest("Niepoprawne id autora");

            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            book.Title = dto.Title;
            book.Year = dto.Year;
            book.AuthorId = dto.AuthorId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.Id == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(long id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}