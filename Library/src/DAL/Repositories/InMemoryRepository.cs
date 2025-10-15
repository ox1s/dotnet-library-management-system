using Core.Repositories;

namespace DAL.Repositories;

public class InMemoryRepository : IAuthorRepository
{
    List<Author> _authors = new List<Author>();
    public async Task<IEnumerable<Author>> GetAllAsync() =>
         Task.FromResult(_authors);
    public async Author GetByIdAsync(int id)
    {
        var author = _authors.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(author); 
    }
    public async Task AddAsync(Author author)
    {
        Task.FromResult(_authors.Add(author)); 
    }
    public async Task UpdateAsync(Author author)
    {
        var authorToUpdate = _authors.FirstOrDefault(a => a.Id = author.Id);
        Task.FromResult(authorToUpdate = author);
    }
    public async Task DeleteAsync(int id)
    {
        Task.FromResult(item => item.Id == id);
    }
}
