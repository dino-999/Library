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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            var authors = await _authorService.GetAuthorsAsync();
            var authorDTOs = MapToAuthorDTOs(authors);
            return Ok(authorDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            var authorDTO = MapToAuthorDTO(author);
            return Ok(authorDTO);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> PostAuthor(AuthorDTO authorDTO)
        {
            var author = MapToAuthor(authorDTO);
            var addedAuthor = await _authorService.AddAuthorAsync(author);
            var addedAuthorDTO = MapToAuthorDTO(addedAuthor);
            return CreatedAtAction(nameof(GetAuthor), new { id = addedAuthorDTO.AuthorId }, addedAuthorDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorDTO authorDTO)
        {
            if (id != authorDTO.AuthorId)
            {
                return BadRequest();
            }
            var author = MapToAuthor(authorDTO);
            await _authorService.UpdateAuthorAsync( author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleted = await _authorService.DeleteAuthorAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
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
                Books = author.Books?.Select(book => new BookDTO
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Year = book.Year
                }).ToList() ?? new List<BookDTO>()
            };
        }


        private List<AuthorDTO> MapToAuthorDTOs(IEnumerable<Author> authors)
        {
            if (authors == null)
            {
                return new List<AuthorDTO>();
            }

            return authors.Select(author => MapToAuthorDTO(author)).Where(dto => dto != null).ToList();
        }


        private Author MapToAuthor(AuthorDTO authorDTO)
        {
            if (authorDTO == null)
            {
                return null;
            }

            return new Author
            {
                AuthorId = authorDTO.AuthorId,
                FirstName = authorDTO.FirstName,
                LastName = authorDTO.LastName,
                DateOfBirth = authorDTO.DateOfBirth,
                Title = authorDTO.Title,
                Books = authorDTO.Books?.Select(bookDTO => new Book
                {
                    BookId = bookDTO.BookId,
                    Title = bookDTO.Title,
                    Year = bookDTO.Year
                }).ToList() ?? new List<Book>()
            };
        }

    }
}
