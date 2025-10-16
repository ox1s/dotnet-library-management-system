using Core.Interfaces;
using Core.DTOs;
using Core.Entities;

namespace BLL.Services;

public class AuthorsService : IAuthorsService
{
    private readonly IAuthorsRepository _authorsRepository;

    public AuthorsService(IAuthorsRepository authorRepository)
    {
        _authorsRepository = authorRepository;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
    {
        var authorsEntities = await _authorsRepository.GetAllAsync();

        var authorsDtos = authorsEntities.Select(author => new AuthorDto(
            author.Id,
            author.Name,
            author.DateOfBirth
        ));

        return authorsDtos;
    }
    public async Task<AuthorDto?> GetAuthorByIdAsync(long id)
    {
        var author = await _authorsRepository.GetByIdAsync(id);
        if (author == null) return null;
        return new AuthorDto(
                author.Id,
                author.Name,
                author.DateOfBirth);
    }

    public async Task<AuthorDto> AddAuthorAsync(CreateAuthorDto authorDto)
    {
        var authorToAdd = new Author
        {
            Name = authorDto.Name,
            DateOfBirth = authorDto.DateOfBirth
        };

        await _authorsRepository.AddAsync(authorToAdd);

        return new AuthorDto(
                authorToAdd.Id,
                authorToAdd.Name,
                authorToAdd.DateOfBirth);
    }
    public async Task UpdateAuthorInformationAsync(AuthorDto authorDto)
    {

        var authorToUpdate = new Author
        {
            Id = authorDto.Id,
            Name = authorDto.Name,
            DateOfBirth = authorDto.DateOfBirth
        };

        await _authorsRepository.UpdateAsync(authorToUpdate);
    }
    public async Task DeleteAuthorAsync(AuthorDto authorDto)
    {
        
        var authorToDelete = new Author
        {
            Id = authorDto.Id,
            Name = authorDto.Name,
            DateOfBirth = authorDto.DateOfBirth
        };

        await _authorsRepository.DeleteAsync(authorToDelete);
    }
}

