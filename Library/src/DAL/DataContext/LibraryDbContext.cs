using Library.Core.Entities;
using Library.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.DataContext;

public class LibraryDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;

    public LibraryDbContext(DbContextOptions options) : base(options)
    { }

    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }
}
