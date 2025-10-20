namespace Library.Core.Interfaces;

public interface IUnitOfWork
{
    IAuthorRepository AuthorRepository { get; }
    IBookRepository BookRepository { get; }

    Task<int> CommitChangesAsync(); 
}