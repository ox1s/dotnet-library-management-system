using Library.BLL.DataContext;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Repositories;

public class MSSqlAuthorRepository : IAuthorRepository
{
    private readonly MSSqlAuthorDbContext _dbContext;

    public MSSqlAuthorRepository(MSSqlAuthorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Author author)
    {
        await _dbContext.AddAsync(author);
    }

    public async Task<bool> ExistsAsync(long id)
    {
        return await _dbContext.Authors
            .AsNoTracking()
            .AnyAsync(author => author.Id == id);
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _dbContext.Authors.ToListAsync();
    }

    public async Task<Author?> GetByIdAsync(long id)
    {
        return await
            _dbContext.Authors.FirstOrDefaultAsync(author => author.Id == id);

    }

    public Task DeleteAsync(long id)
    {
        _dbContext.Remove(id);
        return Task.CompletedTask;
    }
    public Task UpdateAsync(Author author)
    {
        _dbContext.Update(author);
        return Task.CompletedTask;
    }

}
