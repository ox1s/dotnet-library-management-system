using System.Threading.Tasks;
using Library.Models;

namespace Library.Repositories;

public class InMemoryAuthorRepository : IAuthorRepository
{
    List<Author> _authors;
    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await Task.Result
    }
    public async Author GetByIdAsync(int id)
    {
        return null;
    }
    public async Task AddAsync(Author author)
    {
        
    }    
    public async Task UpdateAsync(Author author)
    {
    }
    public async Task DeleteAsync(int id)
    {
    }
}
