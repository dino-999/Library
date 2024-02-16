using Library.Database;
using Library.DTOs;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BookController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _context.Books
                .Include(book => book.Author)
                .ToListAsync();

            var bookDTOs = books.Select(book => new BookDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                Year = book.Year,
                Author = new AuthorDTO
                {
                    AuthorId = book.Author.AuthorId,
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName,
                    DateOfBirth = book.Author.DateOfBirth,
                    Title = book.Author.Title
                }
            }).ToList();

            return bookDTOs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDTO = new BookDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                Year = book.Year,
                Author = new AuthorDTO
                {
                    AuthorId = book.Author.AuthorId,
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName,
                    DateOfBirth = book.Author.DateOfBirth,
                    Title = book.Author.Title
                }
            };

            return bookDTO;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
