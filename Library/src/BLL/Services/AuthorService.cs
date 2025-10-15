using Core.Interfaces;
using Core.Entities;

namespace BLL.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepositiry;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepositiry = authorRepository;
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return null;
    }
    public async Task<Author> GetAuthorByIdAsync(long id)
    {
        var author = await _authorRepositiry.GetByIdAsync(id);
        return new Author
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };
    }

    public async Task<Author> AddAuthorAsync(Author author)
    {
        var authorToAdd = new Author
        {
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };

        await _authorRepositiry.AddAsync(authorToAdd);

        return new Author
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };
    }
    public Task UpdateAuthorAsync(Author author) =>
    null;
    public Task DeleteAuthorAsync(long id) =>
    null;
}

