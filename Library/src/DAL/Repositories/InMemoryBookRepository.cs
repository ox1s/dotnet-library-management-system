using Library.Core.Interfaces;
using Library.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.DAL.Repositories;

public class InMemoryBookRepository : IBookRepository
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
        var indexToUpdate = _books.FindIndex(x => x.Id == book.Id);
        _books[indexToUpdate] = book;
        return Task.CompletedTask;
    }
    public Task DeleteAsync(long id)
    {
        _books.RemoveAll(Book => Book.Id == id);
        return Task.CompletedTask;
    }
}
