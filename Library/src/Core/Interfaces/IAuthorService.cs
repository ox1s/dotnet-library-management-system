using Library.Core.DTOs;

namespace Library.Core.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
    Task<AuthorDto> GetAuthorByIdAsync(long id);
    Task<AuthorDto> AddAuthorAsync(CreateAuthorDto authorDto);
    Task UpdateAuthorInformationAsync(long id, UpdateAuthorDto authorDto);
    Task DeleteAuthorAsync(long id);

    // EFCore
    Task<IEnumerable<AuthorWithBookCountDto>> GetAllAuthorsWithBookCountAsync();
    Task<IEnumerable<AuthorDto>> GetAuthorsByNameAsync(string name);

}
