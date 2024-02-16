using Library.DTOs;
using Library.Models;
using Library.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _bookService.GetBooksAsync();
            var bookDTOs = MapToBookDTOs(books);
            return Ok(bookDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _bookService.GetBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookDTO = MapToBookDTO(book);
            return Ok(bookDTO);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> PostBook(BookDTO bookDTO)
        {
            var book = MapToBook(bookDTO);
            var addedBook = await _bookService.AddBookAsync(book);
            var addedBookDTO = MapToBookDTO(addedBook);
            return CreatedAtAction(nameof(GetBook), new { id = addedBookDTO.BookId }, addedBookDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDTO bookDTO)
        {
            if (id != bookDTO.BookId)
            {
                return BadRequest();
            }
            var book = MapToBook(bookDTO);
            await _bookService.UpdateBookAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await _bookService.DeleteBookAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        private BookDTO MapToBookDTO(Book book)
        {
            if (book == null)
            {
                return null;
            }

            return new BookDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                Year = book.Year,
                Author = MapToAuthorDTO(book.Author)
            };
        }

        private List<BookDTO> MapToBookDTOs(IEnumerable<Book> books)
        {
            if (books == null)
            {
                return new List<BookDTO>();
            }

            return books.Select(book => MapToBookDTO(book)).ToList();
        }

        private AuthorDTO MapToAuthorDTO(Author author)
        {
            if (author == null)
            {
                return null;
            }

            return new AuthorDTO
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,
                Title = author.Title,
            };
        }

        private Book MapToBook(BookDTO bookDTO)
        {
            if (bookDTO == null)
            {
                return null;
            }

            // Create the Author object
            Author author = new Author
            {
                AuthorId = bookDTO.Author.AuthorId,
                FirstName = bookDTO.Author.FirstName,
                LastName = bookDTO.Author.LastName,
                DateOfBirth = bookDTO.Author.DateOfBirth,
                Title = bookDTO.Author.Title
            };

            return new Book
            {
                BookId = bookDTO.BookId,
                Title = bookDTO.Title,
                Year = bookDTO.Year,
                Author = author
            };
        }



    }
}
