using System.Data.Common;
using Library.Database;
using Library.Service;
using Microsoft.EntityFrameworkCore;

namespace LibraryTest;

public abstract class LibraryTestBase
{
    protected readonly IBookService BookService;
    protected readonly IAuthorService AuthorService;
    protected readonly DatabaseContext Context;

    protected LibraryTestBase()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        Context = new DatabaseContext(options);
        BookService = new BookService(Context);
        AuthorService = new AuthorService(Context);
    }
}