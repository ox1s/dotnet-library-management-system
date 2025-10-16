using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Core.Entities;

public class Book
{
    public long Id { get; set; }
    [Required]
    public string? Title { get; set; }
    public int PublishedYear { get; set; }
    
    
    // FK
    public long AuthorId { get; set; }
    
    [ForeignKey("AuthorId")]
    public Author? Author { get; set; }
}