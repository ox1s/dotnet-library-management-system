using Library.Core.Interfaces;
using Library.Core.DTOs;
using Library.Core.Entities;

namespace Library.BLL.Services;

public class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;
    public BookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var booksEntities = await _unitOfWork.BookRepository.GetAllAsync();

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
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (book == null)
        {
            throw new AbsentBookException
                ($"Книжки с id={id} не существует");
        }
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(book.AuthorId);
        if (author == null)
        {
            throw new AbsentAuthorException
                ($"Автора с id={id} не существует");
        }

        return new BookDto(
                book.Id,
                book.Title,
                book.PublishedYear,
                book.AuthorId
            );
    }

    public async Task<BookDto> AddBookAsync(CreateBookDto bookDto)
    {
        var existingAuthorsOfBooks = await _unitOfWork.BookRepository.GetAllAsync();

        if (existingAuthorsOfBooks.Any(book => book.AuthorId == bookDto.AuthorId && book.Title == bookDto.Title))
        {
            if (await _unitOfWork.AuthorRepository.ExistsAsync(bookDto.AuthorId))
            {
                var author = await _unitOfWork.AuthorRepository.GetByIdAsync(bookDto.AuthorId);
                if (author is not null)
                {
                    throw new DuplicateBookException
                        ($"Книга с названием '{bookDto.Title}' уже существует у автора {author.Name}.");
                }
            }
        }

        var authorOfBook = await _unitOfWork.AuthorRepository.GetByIdAsync(bookDto.AuthorId);
        if (authorOfBook == null)
        {
            throw new AbsentAuthorException
                ($"Автора с id={bookDto.AuthorId} не существует");
        }

        var bookToAdd = new Book
        {
            Title = bookDto.Title,
            PublishedYear = bookDto.PublishedYear,
            AuthorId = bookDto.AuthorId,
        };

        await _unitOfWork.BookRepository.AddAsync(bookToAdd);
        await _unitOfWork.CommitChangesAsync();
        return new BookDto(
                bookToAdd.Id,
                bookToAdd.Title,
                bookToAdd.PublishedYear,
                bookToAdd.AuthorId
                );

    }
    public async Task UpdateBookInformationAsync(BookDto bookDto)
    {
        var authorOfBook = await _unitOfWork.AuthorRepository.GetByIdAsync(bookDto.AuthorId);
        if (authorOfBook == null)
        {
            throw new AbsentAuthorException
                ($"Автора с id={bookDto.AuthorId} не существует");
        }

        var bookToUpdate = await _unitOfWork.BookRepository.GetByIdAsync(bookDto.Id);

        if (bookToUpdate == null)
        {
            throw new AbsentBookException
                ($"Невозможно обносить. Автор с id={bookDto.Id} не существует.");
        }

        bookToUpdate.Title = bookDto.Title;
        bookToUpdate.PublishedYear = bookDto.PublishedYear;
        bookToUpdate.AuthorId = bookDto.AuthorId;

        await _unitOfWork.BookRepository.UpdateAsync(bookToUpdate);
        await _unitOfWork.CommitChangesAsync();
    }
    public async Task DeleteBookAsync(long id)
    {
        var bookToDelete = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (bookToDelete == null)
        {
            throw new AbsentBookException
                ($"Книжки с id={id} не существует");
        }

        await _unitOfWork.BookRepository.DeleteAsync(id);
        await _unitOfWork.CommitChangesAsync();
    }
}

