using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces;

public interface IBooksService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<BookDto> GetBookByIdAsync(long id);
    Task<BookDto> AddBookAsync(BookDto bookDto);
    Task UpdateBookInformationAsync(BookDto bookDto);
    Task DeleteBookAsync(long id);
}
