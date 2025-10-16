using Core.Entities;

namespace Core.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(long id);
    Task<Book> AddBookAsync(Book book);
    Task UpdateBookInformationAsync(Book book);
    Task DeleteBookAsync(long id);
}
