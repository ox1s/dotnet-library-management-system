using Library.Core.Interfaces;
using Library.Core.Entities;

namespace Library.DAL.Repositories;

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
        var indexToUpdate = _authors.FindIndex(x => x.Id == author.Id);
        _authors[indexToUpdate] = author;
        return Task.CompletedTask;
    }
    public Task DeleteAsync(long id)
    {
        _authors.RemoveAll(x => x.Id == id);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(long id) =>
        Task.FromResult(_authors.Any(author => author.Id == id));


    // Методы нужны для задания с EFCore. При обновлении решила не удалять
    public Task<IEnumerable<Author>> GetAllWithBooksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Author>> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

}
