using FluentAssertions;
using Library.BLL.Exceptions;
using Library.BLL.Services;
using Library.Core.DTOs;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Moq;

namespace Library.Tests;

public class AuthorServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly AuthorService _authorService;

    public AuthorServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _unitOfWorkMock.Setup(uow => uow.AuthorRepository).Returns(_authorRepositoryMock.Object);
        _authorService = new AuthorService(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task AddAuthorAsync_WhenAuthorIsUnique_ShouldAddAuthor()
    {
        // Arrange
        var createDto = new CreateAuthorDto("Новый автор", new DateOnly(1990, 1, 1));

        _authorRepositoryMock
            .Setup(rep => rep.GetAllAsync())
            .ReturnsAsync(new List<Author>());

        // Act
        var resultDto = await _authorService.AddAuthorAsync(createDto);

        // Assert
        resultDto.Should().NotBeNull();
        resultDto.Name.Should().Be(createDto.Name);

        _unitOfWorkMock.Verify(uow => uow.AuthorRepository.AddAsync(It.IsAny<Author>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CommitChangesAsync(), Times.Once);
    }
    [Fact]
    public async Task AddAuthorAsync_WhenAuthorNameIsEmpty_ShouldThrowAbsentNameAuthorException()
    {
        // Arrange
        var createDto = new CreateAuthorDto("", new DateOnly(1990, 1, 1));

        // Act
        Func<Task> act = () => _authorService.AddAuthorAsync(createDto);

        // Assert
        await act.Should().ThrowAsync<AbsentNameAuthorException>()
                 .WithMessage("Автор без имени не корректен");

        _unitOfWorkMock.Verify(uow => uow.AuthorRepository.AddAsync(It.IsAny<Author>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.CommitChangesAsync(), Times.Never);
    }
    [Fact]
    public async Task AddAuthorAsync_WhenAuthorDateOfBirthIsFuture_ShouldThrowImpossibleDateException()
    {
        // Arrange
        var createDto = new CreateAuthorDto("Человек из будущего", new DateOnly(2072, 1, 1));

        _authorRepositoryMock
          .Setup(repo => repo.ExistsByNameAndBirthDateAsync(It.IsAny<string>(), It.IsAny<DateOnly>(), null))
          .ReturnsAsync(false);
        
        // Act
        Func<Task> act = () => _authorService.AddAuthorAsync(createDto);

        // Assert
        await act.Should().ThrowAsync<ImpossibleDateException>()
                 .WithMessage("Дата рождения не может быть в будущем");

        _unitOfWorkMock.Verify(uow => uow.AuthorRepository.AddAsync(It.IsAny<Author>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.CommitChangesAsync(), Times.Never);
    }

}
