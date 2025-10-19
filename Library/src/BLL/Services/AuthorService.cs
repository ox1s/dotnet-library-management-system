using Library.Core.Interfaces;
using Library.Core.DTOs;
using Library.Core.Entities;

namespace Library.BLL.Services;

public class AuthorService : IAuthorService
{
    private readonly IUnitOfWork _unitOfWork;
    public AuthorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
    {
        var authorsEntities = await _unitOfWork.AuthorRepository.GetAllAsync();

        var authorsDtos = authorsEntities.Select(author => new AuthorDto(
            author.Id,
            author.Name,
            author.DateOfBirth
        ));
        return authorsDtos;
    }
    public async Task<AuthorDto> GetAuthorByIdAsync(long id)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
        if (author is null)
        {
            throw new AbsentAuthorException
                ($"Автора с id={id} не существует");
        }
        return new AuthorDto(
                author.Id,
                author.Name,
                author.DateOfBirth);
    }

    public async Task<AuthorDto> AddAuthorAsync(CreateAuthorDto authorDto)
    {
        var existingAuthors = await _unitOfWork.AuthorRepository.GetAllAsync();

        if (existingAuthors.Any(author => author.Name == authorDto.Name))
        {
            throw new DuplicateAuthorException
                ($"Автор с именем '{authorDto.Name}' уже существует");
        }

        var authorToAdd = new Author
        {
            Name = authorDto.Name,
            DateOfBirth = authorDto.DateOfBirth
        };

        await _unitOfWork.AuthorRepository.AddAsync(authorToAdd);
        await _unitOfWork.CommitChangesAsync();
        return new AuthorDto(
                authorToAdd.Id,
                authorToAdd.Name,
                authorToAdd.DateOfBirth);

    }
    public async Task UpdateAuthorInformationAsync(long id, UpdateAuthorDto authorDto)
    {
        var authorToUpdate = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

        if (authorToUpdate == null)
        {
            throw new AbsentAuthorException
                ($"Невозможно обносить. Автор с id={id} не существует");
        }

        authorToUpdate.Name = authorDto.Name;
        authorToUpdate.DateOfBirth = authorDto.DateOfBirth;

        await _unitOfWork.AuthorRepository.UpdateAsync(authorToUpdate);
        await _unitOfWork.CommitChangesAsync();
    }
    public async Task DeleteAuthorAsync(long id)
    {
        var authorToDelete = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
        if (authorToDelete == null)
        {
            throw new AbsentAuthorException
                ($"Автора с id={id} не существует");
        }

        await _unitOfWork.AuthorRepository.DeleteAsync(id);
        await _unitOfWork.CommitChangesAsync();
    }

    public async Task<IEnumerable<AuthorWithBookCountDto>> GetAllAuthorsWithBookCountAsync()
    {
        var authorsWithBooks = await _unitOfWork.AuthorRepository.GetAllWithBooksAsync();

        return authorsWithBooks.Select(a => new AuthorWithBookCountDto(
            a.Id,
            a.Name,
            a.Books.Count
        ));
    }

    public async Task<IEnumerable<AuthorDto>> GetAuthorsByNameAsync(string name)
    {
        var authorsEntities = await _unitOfWork.AuthorRepository.GetByNameAsync(name);

        var authorsDtos = authorsEntities.Select(author => new AuthorDto(
            author.Id,
            author.Name,
            author.DateOfBirth
        ));
        return authorsDtos;
    }
}

