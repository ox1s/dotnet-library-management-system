using Library.Core.Entities;

namespace Library.Core.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(long id);
    Task<bool> ExistsAsync(long id);
    Task AddAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(long id);
}
