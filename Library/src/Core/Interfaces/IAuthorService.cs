using Library.Core.Entities;
using Library.Core.DTOs;

namespace Library.Core.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
    Task<AuthorDto> GetAuthorByIdAsync(long id);
    Task<AuthorDto> AddAuthorAsync(CreateAuthorDto authorDto);
    Task UpdateAuthorInformationAsync(AuthorDto authorDto);
    Task DeleteAuthorAsync(long id);
}
