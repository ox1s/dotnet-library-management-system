using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.DataContext;

public class LibraryDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;

    public LibraryDbContext(DbContextOptions options) : base(options)
    { }

}
