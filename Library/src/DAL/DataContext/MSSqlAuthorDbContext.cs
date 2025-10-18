using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.DataContext;

public class MSSqlAuthorDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }

    public MSSqlAuthorDbContext(DbContextOptions<MSSqlAuthorDbContext> options) 
        : base(options)
    { }
}
