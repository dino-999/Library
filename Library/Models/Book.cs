using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class Book
{
    public int BookId { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    [Range(1000, 3000)]
    public int Year { get; set; }
    public Author Author { get; set; }
}