

using Library.Models;
namespace Library.Repositories;
public interface IAuthorRepository
{
    IEnumerable<Author> GetAllAsync();
    Author GetByIdAsync(int id);
    Task AddAsync(Author author);
    Task UpdateAsync(Author author); 
    Task DeleteAsync(int id);
}
