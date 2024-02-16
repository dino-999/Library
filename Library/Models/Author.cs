using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class Author
{
    public int AuthorId { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [StringLength(50)]
    public string Title { get; set; }
    
    public ICollection<Book> Books { get; set; }
}