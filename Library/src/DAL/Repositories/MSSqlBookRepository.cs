using Library.BLL.DataContext;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repositories;

public class MSSqlBookRepository : IBookRepository
{
    private readonly LibraryDbContext _dbContext;

    public MSSqlBookRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Book author)
    {
        await _dbContext.Books.AddAsync(author);
    }

    public async Task<bool> ExistsAsync(long id)
    {
        return await _dbContext.Books
            .AsNoTracking()
            .AnyAsync(author => author.Id == id);
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _dbContext.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(long id)
    {
        return await
            _dbContext.Books.FirstOrDefaultAsync(author => author.Id == id);

    }

    public async Task DeleteAsync(long id)
    {
        var authorToDelete = await _dbContext.Books.FindAsync(id);
        if (authorToDelete != null)
        {
            _dbContext.Books.Remove(authorToDelete);
        }
    }
    public Task UpdateAsync(Book author)
    {
        _dbContext.Update(author);
        return Task.CompletedTask;
    }
}

