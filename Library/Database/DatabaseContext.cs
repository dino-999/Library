using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Library.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public void SeedData()
        {
            if (!Authors.Any() && !Books.Any())
            {
                // Create authors
                var authors = new[]
                {
                    new Author { FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Parse("1980-01-01"), Title = "Dr." },
                    new Author { FirstName = "Jane", LastName = "Smith", DateOfBirth = DateTime.Parse("1975-05-10"), Title = "Prof." }
                    // Add more authors if needed
                };
                Authors.AddRange(authors);
                SaveChanges();
        
                // Create books for the first author
                var johnsBooks = new[]
                {
                    new Book { Title = "Book 1 by John", Year = 2022, Author = authors[0] },
                    new Book { Title = "Book 2 by John", Year = 2020, Author = authors[0] },
                    // Add more books for John here
                };
                Books.AddRange(johnsBooks);
                authors[0].Books = johnsBooks;
        
                // Create books for the second author
                var janesBooks = new[]
                {
                    new Book { Title = "Book 1 by Jane", Year = 2021, Author = authors[1] },
                    new Book { Title = "Book 2 by Jane", Year = 2019, Author = authors[1] },
                    // Add more books for Jane here
                };
                Books.AddRange(janesBooks);
                authors[1].Books = janesBooks;
        
                SaveChanges();
            }
        }

    }
}