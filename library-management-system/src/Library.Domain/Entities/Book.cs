namespace Library.Models;

public class Book
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public int PublishedYear { get; set; }
    
    public long AuthorId { get; set; }
}
