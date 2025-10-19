using FluentAssertions;
using Library.BLL.Exceptions;
using Library.BLL.Services;
using Library.Core.DTOs;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.DAL.Repositories;

namespace Library.Tests;

public class AuthorService_InMemoryTests
{
    [Fact]
    public async Task AddAuthorAsync_WhenAddingTwoAuthors_ShouldContainTwoAuthors()
    {
        // Arrange
        var unitOfWork = new InMemoryUnitOfWork();
        var service = new AuthorService(unitOfWork);

        var author1 = new CreateAuthorDto("Александр Сергеевич Пушкин", new DateOnly(1799, 06, 06));
        var author2 = new CreateAuthorDto("Николай Алексеевич Некрасов", new DateOnly(1821, 12, 10));

        // Act
        await service.AddAuthorAsync(author1);
        await service.AddAuthorAsync(author2);

        // Assert
        var allAuthors = await service.GetAllAuthorsAsync();
        allAuthors.Should().HaveCount(2);
    }
    [Fact]
    public async Task AddBookAsync_WhenAuthorDoesNotExist_ShouldThrowAbsentAuthorException()
    {
        // Arrange
        var unitOfWork = new InMemoryUnitOfWork();
        var service = new BookService(unitOfWork);
        var book = new CreateBookDto("Title", 1921, 999);

        // Act
        Func<Task> act = async () => await service.AddBookAsync(book);

        // Assert
        await act.Should().ThrowAsync<AbsentAuthorException>()
                 .WithMessage("Автор с id=999 не существует.");
    }
    [Fact]
    public async Task AddBookAsync_WhenPublishedYearLaterDateOfBirth_ShouldThrowImpossibleDateException()
    {
        // Arrange
        var unitOfWork = new InMemoryUnitOfWork();
        var bookService = new BookService(unitOfWork);
        var authorService = new AuthorService(unitOfWork);

        var author = new CreateAuthorDto("Пушкин Бла-Бла", new DateOnly(1799, 06, 06));
        var book = new CreateBookDto("Нейкая книжка", 1791, 1);

        // Act
        await authorService.AddAuthorAsync(author);
        Func<Task> act = async () => await bookService.AddBookAsync(book);

        // Assert
        await act.Should().ThrowAsync<ImpossibleDateException>()
                 .WithMessage("Если что-то и написал, то во снах матери. Дата публикации раньше года рождения!");
    }
}
