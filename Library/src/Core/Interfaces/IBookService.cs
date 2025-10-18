using Library.Core.DTOs;
using Library.Core.Entities;

namespace Library.Core.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<BookDto> GetBookByIdAsync(long id);
    Task<BookDto> AddBookAsync(CreateBookDto bookDto);
    Task UpdateBookInformationAsync(BookDto bookDto);
    Task DeleteBookAsync(long id);
}
