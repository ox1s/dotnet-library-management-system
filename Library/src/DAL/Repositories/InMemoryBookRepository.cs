using Library.Core.Interfaces;
using Library.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.DAL.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books;

    public InMemoryBookRepository(List<Book> books)
    {
        _books = books;
    }


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

    public Task<bool> ExistsByAuthorIdAndTitleAsync(string title, long authorId)
    {
        // По моей логике:
        // одинаковые названия у книженций быть могут
        // Но, чтобы они были у одного автора - врятли
        var books = _books
                    .Where(book =>
                    book.Title == title
                    &&
                    book.AuthorId == authorId);
        
        return Task.FromResult(books.Any());
    }

    public Task<IEnumerable<Book>> GetByYearAsync(int year)
    {
        var books = _books
                    .Where(book => book.PublishedYear > year)
                    .ToList();
        return Task.FromResult(books.AsEnumerable());
        
    }
}
