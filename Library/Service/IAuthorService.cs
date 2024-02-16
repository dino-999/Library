using Library.Models;

namespace Library.Service;

public interface IAuthorService
{
    Task<List<Author>> GetAuthorsAsync();
    Task<Author> GetAuthorAsync(int id);
    Task<Author> AddAuthorAsync(Author author);
    Task<bool> UpdateAuthorAsync(Author author);
    Task<bool> DeleteAuthorAsync(int id);
}