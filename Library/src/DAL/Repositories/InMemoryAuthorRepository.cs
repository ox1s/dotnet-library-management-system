using Core.Interfaces;
using Core.Entities;

namespace DAL.Repositories;

public class InMemoryAuthorRepository : IAuthorRepository
{
    List<Author> _authors = new List<Author>();

    private long _currentId = 1;
    public Task<IEnumerable<Author>> GetAllAsync() =>
        Task.FromResult(_authors.AsEnumerable());

    public Task<Author?> GetByIdAsync(long id) =>
        Task.FromResult(_authors.FirstOrDefault(x => x.Id == id));
    public Task AddAsync(Author author)
    {
        author.Id = _currentId++;
        _authors.Add(author);
        return Task.CompletedTask;
    }
    public Task UpdateAsync(Author author)
    {
        var authorToUpdate = _authors.FirstOrDefault(x => x.Id == author.Id);
        return Task.CompletedTask;
    }
    public Task DeleteAsync(long id)
    {
        _authors.RemoveAll(Author => Author.Id == id);
        return Task.CompletedTask;
    }
}
