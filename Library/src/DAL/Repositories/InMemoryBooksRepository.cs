using Core.Interfaces;
using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories;

public class InMemoryBooksRepository : IBooksRepository
{
    List<Book> _books = new List<Book>();

    private long _currentId = 1;
    public Task<IEnumerable<Book>> GetAllAsync() =>
        Task.FromResult(_books.AsEnumerable());

    public Task<Book?> GetByIdAsync(long id) =>
        Task.FromResult(_books.FirstOrDefault(x => x.Id == id));
    public Task AddAsync(Book book)
    {
        book.Id = _currentId++;
        _books.Add(book);
        return Task.CompletedTask;
    }
    public Task UpdateAsync(Book book)
    {
        var authorToUpdate = _books.FirstOrDefault(x => x.Id == book.Id);
        return Task.CompletedTask;
    }
    public Task DeleteAsync(long id)
    {
        _books.RemoveAll(Book => Book.Id == id);
        return Task.CompletedTask;
    }
}
