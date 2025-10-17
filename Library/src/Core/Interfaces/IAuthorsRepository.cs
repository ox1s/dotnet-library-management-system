using Core.Entities;

namespace Core.Interfaces;

public interface IAuthorsRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(long id);
    Task AddAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(long id);
}
