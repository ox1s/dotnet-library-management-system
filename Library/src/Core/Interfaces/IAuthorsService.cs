using Core.Entities;
using Core.DTOs;

namespace Core.Interfaces;

public interface IAuthorsService
{
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
    Task<AuthorDto?> GetAuthorByIdAsync(long id);
    Task<AuthorDto> AddAuthorAsync(CreateAuthorDto authorDto);
    Task UpdateAuthorInformationAsync(AuthorDto authorDto);
    Task DeleteAuthorAsync(AuthorDto authorDto);
}
