using Library.Core.Interfaces;
using Library.Core.DTOs;
using Library.Core.Entities;

namespace Library.BLL.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
    {
        var authorsEntities = await _authorRepository.GetAllAsync();

        var authorsDtos = authorsEntities.Select(author => new AuthorDto(
            author.Id,
            author.Name,
            author.DateOfBirth
        ));

        return authorsDtos;
    }
    public async Task<AuthorDto> GetAuthorByIdAsync(long id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null) throw new AbsentAuthorException($"Автора с id={id} не существует");
        return new AuthorDto(
                author.Id,
                author.Name,
                author.DateOfBirth);
    }

    public async Task<AuthorDto> AddAuthorAsync(CreateAuthorDto authorDto)
    {
        var existingAuthors = await _authorRepository.GetAllAsync();

        if (existingAuthors.Any(author => author.Name == authorDto.Name))
        {
            throw new DuplicateAuthorException($"Автор с именем '{authorDto.Name}' уже существует.");
        }

        var authorToAdd = new Author
        {
            Name = authorDto.Name,
            DateOfBirth = authorDto.DateOfBirth
        };

        await _authorRepository.AddAsync(authorToAdd);

        return new AuthorDto(
                authorToAdd.Id,
                authorToAdd.Name,
                authorToAdd.DateOfBirth);

    }
    public async Task UpdateAuthorInformationAsync(AuthorDto authorDto)
    {
        var authorToUpdate = await _authorRepository.GetByIdAsync(authorDto.Id);

        if (authorToUpdate == null)
            throw new AbsentAuthorException($"Невозможно обносить. Автор с id={authorDto.Id} не существует.");

        authorToUpdate.Name = authorDto.Name;
        authorToUpdate.DateOfBirth = authorDto.DateOfBirth;

        await _authorRepository.UpdateAsync(authorToUpdate);
    }
    public async Task DeleteAuthorAsync(long id)
    {
        var authorToDelete = await _authorRepository.GetByIdAsync(id);
        if (authorToDelete == null) throw new AbsentAuthorException($"Автора с id={id} не существует");
        await _authorRepository.DeleteAsync(id);
    }
}

