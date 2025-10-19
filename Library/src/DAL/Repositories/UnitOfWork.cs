using Library.DAL.DataContext;
using Library.Core.Interfaces;

namespace Library.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDbContext _dbContext;
    private IAuthorRepository _authorRepository = null!;
    private IBookRepository _bookRepository = null!;

    public UnitOfWork(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IAuthorRepository AuthorRepository => _authorRepository ??= new MSSqlAuthorRepository(_dbContext);
    public IBookRepository BookRepository => _bookRepository ??= new MSSqlBookRepository(_dbContext);

    public Task<int> CommitChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}