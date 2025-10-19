using FluentAssertions;
using Library.BLL;
using Library.BLL.Services;
using Library.Core.DTOs;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.DAL.Repositories;
using Moq;

namespace Library.Tests;

public class BookServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly AuthorService _authorService;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _bookRepositoryMock = new Mock<IBookRepository>();
        _unitOfWorkMock.Setup(uow => uow.AuthorRepository).Returns(_authorRepositoryMock.Object);
        _unitOfWorkMock.Setup(uow => uow.BookRepository).Returns(_bookRepositoryMock.Object);
        _authorService = new AuthorService(_unitOfWorkMock.Object);
        _bookService = new BookService(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task AddBookAsync_WhenAuthorExists_ShouldAddBook()
    {
        // Arrange
        var createBookDto = new CreateBookDto("Новая книга", 2000, 1);
        var existingAuthor = new Author { Id = 1, Name = "Существующий Автор", DateOfBirth = new DateOnly(1700, 12, 02) };

        _authorRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(existingAuthor);

        _bookRepositoryMock
            .Setup(repo => repo.ExistsByAuthorIdAndTitleAsync(It.IsAny<string>(), It.IsAny<long>()))
            .ReturnsAsync(false);

        _unitOfWorkMock
            .Setup(uow => uow.AuthorRepository)
            .Returns(_authorRepositoryMock.Object);
        _unitOfWorkMock
            .Setup(uow => uow.BookRepository)
            .Returns(_bookRepositoryMock.Object);

        // Act
        var resultDto = await _bookService.AddBookAsync(createBookDto);

        // Assert
        resultDto.Should().NotBeNull();
        resultDto.Title.Should().Be(createBookDto.Title);

        _bookRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CommitChangesAsync(), Times.Once);
    }

}
