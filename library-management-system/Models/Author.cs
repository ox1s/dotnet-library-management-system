namespace library_management_system.Models;

public class Author
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Secret { get; set; }
}
