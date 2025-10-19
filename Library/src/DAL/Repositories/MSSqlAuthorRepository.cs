using Library.BLL.DataContext;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repositories;

public class MSSqlAuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _dbContext;

    public MSSqlAuthorRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Author author)
    {
        await _dbContext
                .AddAsync(author);
    }

    public async Task<bool> ExistsAsync(long id)
    {
        return await _dbContext
                        .Authors
                        .AsNoTracking()
                        .AnyAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _dbContext
                        .Authors
                        .ToListAsync();
    }

    public async Task<Author?> GetByIdAsync(long id)
    {
        return await _dbContext
                        .Authors
                        .FirstOrDefaultAsync(a => a.Id == id);

    }

    public async Task DeleteAsync(long id)
    {
        await _dbContext
                .Authors
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();
    }
    public Task UpdateAsync(Author author)
    {
        _dbContext.Update(author);
        return Task.CompletedTask;
    }
    public async Task<bool> ExistsByNameAndBirthDateAsync(string name, DateOnly birthDate, long? excludeId = null)
    {
        var query = _dbContext
                        .Authors
                        .Where(author =>
                        author.Name == name
                        &&
                        author.DateOfBirth == birthDate);

        if (excludeId.HasValue)
        {
            query = query
                    .Where(author =>
                    author.Id != excludeId.Value);
        }

        return await query.AnyAsync();
    }

    // Спец. запросы

    public async Task<IEnumerable<Author>> GetAllWithBooksAsync()
    {
        return await _dbContext
                        .Authors
                        .Include(a => a.Books)
                        .ToListAsync();
    }
    public async Task<IEnumerable<Author>> GetByNameAsync(string name)
    {
        return await _dbContext
                        .Authors
                        .Where(a => a.Name.Contains(name))
                        .ToListAsync();

    }

}
