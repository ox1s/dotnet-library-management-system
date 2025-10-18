using System.ComponentModel.DataAnnotations;

namespace Library.Core.Entities;

public class Author
{
    public long Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
}
