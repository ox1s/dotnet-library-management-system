using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Author
{
    public long Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
