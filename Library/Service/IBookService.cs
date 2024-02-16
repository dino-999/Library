using Library.Models;

namespace Library.Service;

public interface IBookService
{
    Task<List<Book>> GetBooksAsync();
    Task<Book> GetBookAsync(int id);
    Task<Book> AddBookAsync(Book book);
    Task<bool> UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(int id);
}