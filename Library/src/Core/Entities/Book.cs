using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Library.Core.Entities;

public class Book
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public int PublishedYear { get; set; }
    
    
    public long AuthorId { get; set; }
    
    public Author? Author { get; set; }
}