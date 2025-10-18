using Library.Core.Interfaces;
using Library.Core.DTOs;
using Library.Core.Entities;

namespace Library.BLL.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;

    public BookService(IBookRepository bookRepository, IAuthorRepository authorsRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorsRepository;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var booksEntities = await _bookRepository.GetAllAsync();

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
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null) throw new AbsentBookException($"Книжки с id={id} не существует");
        var author = await _authorRepository.GetByIdAsync(book.AuthorId);
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
        var existingAuthorsOfBooks = await _bookRepository.GetAllAsync();

        if (existingAuthorsOfBooks.Any(book => book.AuthorId == bookDto.AuthorId && book.Title == bookDto.Title))
        {
            var author = await _authorRepository.GetByIdAsync(bookDto.AuthorId);
            throw new DuplicateBookException($"Книга с названием '{bookDto.Title}' уже существует у автора {author.Name}.");
        }

        var authorOfBook = await _authorRepository.GetByIdAsync(bookDto.AuthorId);
        if (authorOfBook == null) throw new AbsentAuthorException($"Автора с id={bookDto.AuthorId} не существует");

        var bookToAdd = new Book
        {
            Title = bookDto.Title,
            PublishedYear = bookDto.PublishedYear,
            AuthorId = bookDto.AuthorId,
        };

        await _bookRepository.AddAsync(bookToAdd);

        return new BookDto(
                bookToAdd.Id,
                bookToAdd.Title,
                bookToAdd.PublishedYear,
                bookToAdd.AuthorId
                );

    }
    public async Task UpdateBookInformationAsync(BookDto bookDto)
    {
        var authorOfBook = await _authorRepository.GetByIdAsync(bookDto.AuthorId);
        if (authorOfBook == null)
            throw new AbsentAuthorException($"Автора с id={bookDto.AuthorId} не существует");

        var bookToUpdate = new Book
        {
            Id = bookDto.Id,
            Title = bookDto.Title,
            PublishedYear = bookDto.PublishedYear,
            AuthorId = bookDto.AuthorId
        };

        await _bookRepository.UpdateAsync(bookToUpdate);
    }
    public async Task DeleteBookAsync(long id)
    {
        var bookToDelete = await _bookRepository.GetByIdAsync(id);
        if (bookToDelete == null) throw new AbsentBookException($"Книжки с id={id} не существует");

        await _bookRepository.DeleteAsync(id);
    }
}

