using Core.Entities;

namespace Core.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author> GetAuthorByIdAsync(long id);
    Task AddAuthorAsync(Author author);
    Task UpdateAuthorInformationAsync(Author author);
    Task DeleteAuthorAsync(long id);
}
