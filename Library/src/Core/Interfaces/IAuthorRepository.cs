using Core.Entities;

namespace Core.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(long id);
    Task AddAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(long id);
}
