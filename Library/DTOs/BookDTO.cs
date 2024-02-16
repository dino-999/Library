namespace Library.DTOs;

public class BookDTO
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public AuthorDTO Author { get; set; }
}