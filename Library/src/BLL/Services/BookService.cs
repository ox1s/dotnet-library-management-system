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

        return new BookDto(
                book.Id,
                book.Title,
                book.PublishedYear,
                book.AuthorId
            );
    }

    public async Task<BookDto> AddBookAsync(CreateBookDto bookDto)
    {
        await ValidateTitleWithAuthor(bookDto.Title, bookDto.AuthorId);
        await ValidateDate(bookDto.PublishedYear);

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
    public async Task UpdateBookInformationAsync(long id, UpdateBookDto bookDto)
    {

        var bookToUpdate = await _unitOfWork.BookRepository.GetByIdAsync(id);

        if (bookToUpdate == null)
        {
            throw new AbsentBookException
                ($"Невозможно обновить. Книга с id={id} не существует.");
        }
        await ValidateTitleWithAuthor(bookDto.Title, bookDto.AuthorId);

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

    public async Task<IEnumerable<BookDto>> GetAllBooksAfterYearAsync(int year)
    {
        var booksEntities = await _unitOfWork.BookRepository.GetByYearAsync(year);

        var booksDtos = booksEntities.Select(book => new BookDto(
            book.Id,
            book.Title,
            book.PublishedYear,
            book.AuthorId
        ));
        return booksDtos;
    }

    private async Task ValidateDate(int year)
    {
        if (year >= DateTime.Now.Year)
            throw new ImpossibleDateException
                ("Дата не может быть в будущем.");
    }
    private async Task ValidateTitleWithAuthor(string title, long authorId)
    {
        if (string.IsNullOrEmpty(title))
            throw new AbsentTitleBookException
                ($"Книга без навзания не корректна");


        var existingAuthorsOfBooks = await _unitOfWork.BookRepository.GetAllAsync();
        if (existingAuthorsOfBooks
                .Any(book => book.AuthorId == authorId
                &&
                book.Title == title))
        {
            // По моей логике:
            // одинаковые названия у книженций быть могут
            // Но, чтобы они были у одного автора - врятли
            if (await _unitOfWork.AuthorRepository.ExistsAsync(authorId))
            {
                var author = await _unitOfWork.AuthorRepository.GetByIdAsync(authorId);
                if (author is not null)
                {
                    throw new DuplicateBookException
                        ($"У автора {author.Name} уже существует книга с таким названием {title}");
                }
            }
        }

        var authorOfBook = await _unitOfWork.AuthorRepository.GetByIdAsync(authorId);
        if (authorOfBook == null)
        {
            throw new AbsentAuthorException
                ($"Автора с id={authorId} не существует");
        }
    }

}

