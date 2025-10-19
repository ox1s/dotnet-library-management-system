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
        await ValidateData(bookDto.Title, bookDto.AuthorId, bookDto.PublishedYear);

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
                ($"Невозможно обновить. Книга с id={id} не существует");
        }

        await ValidateData(bookDto.Title, bookDto.AuthorId, bookDto.PublishedYear);

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

    private async Task ValidateData(string title,
                                    long authorId,
                                    int publishedYear)
    {

        if (publishedYear >= DateTime.Now.Year)
            throw new ImpossibleDateException
                ("Дата не может быть в будущем.");

        var authorEntity = await _unitOfWork.AuthorRepository.GetByIdAsync(authorId);
        if (authorEntity is null)
            throw new AbsentAuthorException
                ($"Автор с id={authorId} не существует.");


        if (authorEntity.DateOfBirth.Year > publishedYear)
            throw new ImpossibleDateException
                ("Крякнуть и что-то написать сложновато");


        if (string.IsNullOrEmpty(title))
            throw new AbsentTitleBookException
                ($"Книга без названия не корректна");


        if (await _unitOfWork.BookRepository.ExistsByAuthorIdAndTitleAsync(title, authorId))

            throw new DuplicateBookException
                ($"У автора '{authorEntity.Name}' уже есть книга с названием '{title}'.");


    }
}
