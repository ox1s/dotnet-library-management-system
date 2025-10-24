using Library.Core.Entities;
using Library.Core.Interfaces;

namespace Library.DAL.Repositories;

public class InMemoryUnitOfWork : IUnitOfWork
{
    private readonly List<Author> _authors = new();
    private readonly List<Book> _books = new();

    public IAuthorRepository AuthorRepository { get; }
    public IBookRepository BookRepository { get; }

    public InMemoryUnitOfWork()
    {
        AuthorRepository = new InMemoryAuthorRepository(_authors, _books);
        BookRepository = new InMemoryBookRepository(_books);
    }

    // Заглушка для List
    public Task<int> CommitChangesAsync() => Task.FromResult(0);
}
