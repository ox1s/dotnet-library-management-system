using Library.Core.DTOs;

namespace Library.Core.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<BookDto> GetBookByIdAsync(long id);
    Task<BookDto> AddBookAsync(CreateBookDto bookDto);
    Task UpdateBookInformationAsync(long id, UpdateBookDto bookDto);
    Task DeleteBookAsync(long id);
    // EFCore
    Task<IEnumerable<BookDto>> GetAllBooksAfterYearAsync(int year);

}
