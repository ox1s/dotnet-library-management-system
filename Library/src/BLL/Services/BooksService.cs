using Core.Interfaces;
using Core.DTOs;
using Core.Entities;

namespace BLL.Services;

public class BooksService : IBooksService
{
    private readonly IBooksRepository _booksRepository;
    private readonly IAuthorsRepository _authorsRepository;

    public BooksService(IBooksRepository bookRepository, IAuthorsRepository authorsRepository)
    {
        _booksRepository = bookRepository;
        _authorsRepository = authorsRepository;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var booksEntities = await _booksRepository.GetAllAsync();

        var booksDtos = booksEntities.Select(book => new BookDto(
            book.Id,
            book.Title,
            book.PublishedYear,
            book.AuthorId
        ));

        return booksDtos;
    }
    public async Task<BookDto> GetBookByIdAsync(long id)
    {
        var book = await _booksRepository.GetByIdAsync(id);
        if (book == null) throw new AbsentBookException($"Книжки с id={id} не существует");
        var author = await _authorsRepository.GetByIdAsync(book.AuthorId);
        if (author == null) throw new AbsentAuthorException($"Автора с id={id} не существует");
        return new BookDto(
                book.Id,
                book.Title,
                book.PublishedYear,
                book.AuthorId
            );
    }

    public async Task<BookDto> AddBookAsync(CreateBookDto bookDto)
    {
        var existingAuthorsOfBooks = await _booksRepository.GetAllAsync();

        if (existingAuthorsOfBooks.Any(book => book.AuthorId == bookDto.AuthorId && book.Title == bookDto.Title))
        {
            var author = await _authorsRepository.GetByIdAsync(bookDto.AuthorId);
            throw new DuplicateBookException($"Книга с названием '{bookDto.Title}' уже существует у автора {author.Name}.");
        }

        var authorOfBook = await _authorsRepository.GetByIdAsync(bookDto.AuthorId);
        if (authorOfBook == null) throw new AbsentAuthorException($"Автора с id={bookDto.AuthorId} не существует");

        var bookToAdd = new Book
        {
            Title = bookDto.Title,
            PublishedYear = bookDto.PublishedYear,
            AuthorId = bookDto.AuthorId,
        };

        await _booksRepository.AddAsync(bookToAdd);

        return new BookDto(
                bookToAdd.Id,
                bookToAdd.Title,
                bookToAdd.PublishedYear,
                bookToAdd.AuthorId
                );

    }
    public async Task UpdateBookInformationAsync(BookDto bookDto)
    {
        var authorOfBook = await _authorsRepository.GetByIdAsync(bookDto.AuthorId);
        if (authorOfBook == null) throw new AbsentAuthorException($"Автора с id={bookDto.AuthorId} не существует");

        var bookToUpdate = new Book
        {
            Id = bookDto.Id,
            Title = bookDto.Title,
            PublishedYear = bookDto.PublishedYear,
            AuthorId = bookDto.AuthorId
        };

        await _booksRepository.UpdateAsync(bookToUpdate);
    }
    public async Task DeleteBookAsync(long id)
    {
        var bookToDelete = await _booksRepository.GetByIdAsync(id);
        if (bookToDelete == null) throw new AbsentBookException($"Книжки с id={id} не существует");
        
        await _booksRepository.DeleteAsync(id);
    }
}

