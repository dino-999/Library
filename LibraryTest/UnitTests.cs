using Library.Models;

namespace LibraryTest;

public class UnitTests : LibraryTestBase
{
    [Fact]
    public async void AddBookAsync_Should_AddBookCorrectly()
    {
        var book = new Book { Title = "Test Book", Year = 2022 };
        
        var addedBook = await BookService.AddBookAsync(book);
        
        Assert.NotNull(addedBook);
        Assert.Equal(book.Title, addedBook.Title);
    }

    [Fact]
    public async void AddAuthorAsync_Should_AddAuthorCorrectly()
    {
        var author = new Author { FirstName = "John", LastName = "Doe", Title = "Dr"};


        var addedAuthor = await AuthorService.AddAuthorAsync(author);

        Assert.NotNull(addedAuthor);
        Assert.Equal(author.FirstName, addedAuthor.FirstName);
    }
    
}