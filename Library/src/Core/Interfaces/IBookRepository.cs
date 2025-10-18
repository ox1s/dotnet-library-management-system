using Library.Core.Entities;

namespace Library.Core.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(long id);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(long id);
}
