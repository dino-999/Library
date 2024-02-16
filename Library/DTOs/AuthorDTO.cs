namespace Library.DTOs;

public class AuthorDTO
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Title { get; set; }
    public List<BookDTO> Books { get; set; }
}