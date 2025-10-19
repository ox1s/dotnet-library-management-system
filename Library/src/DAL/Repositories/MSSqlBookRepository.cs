using Library.DAL.DataContext;
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
                        .AnyAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _dbContext
                        .Books
                        .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(long id)
    {
        return await
            _dbContext
                .Books
                .FirstOrDefaultAsync(b => b.Id == id);

    }

    public async Task DeleteAsync(long id)
    {
        await _dbContext
                .Books
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();
    }
    public Task UpdateAsync(Book author)
    {
        _dbContext.Update(author);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsByAuthorIdAndTitleAsync(string title, long authorId)
    {
        // По моей логике:
        // одинаковые названия у книженций быть могут
        // Но, чтобы они были у одного автора - врятли
        var query = _dbContext
                        .Books
                        .Where(book =>
                        book.Title == title
                        &&
                        book.AuthorId == authorId);
        
        return await query.AnyAsync();
    }

    public async Task<IEnumerable<Book>> GetByYearAsync(int year)
    {
        return await _dbContext
                        .Books
                        .Where(b => b.PublishedYear > year)
                        .ToListAsync();
    }


}

