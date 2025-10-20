namespace Library.Core.Entities;

public class Author
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
