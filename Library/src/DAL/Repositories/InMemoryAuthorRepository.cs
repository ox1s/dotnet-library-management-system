using Library.Core.Interfaces;
using Library.Core.Entities;

namespace Library.DAL.Repositories;

public class InMemoryAuthorRepository : IAuthorRepository
{
    private readonly List<Author> _authors;
    private readonly List<Book> _books;

    public InMemoryAuthorRepository(List<Author> authors, List<Book> books)
    {
        _authors = authors;
        _books = books;
    }

    private long _currentId = 1;
    public Task<IEnumerable<Author>> GetAllAsync() =>
        Task.FromResult(_authors.AsEnumerable());

    public Task<Author?> GetByIdAsync(long id) =>
        Task.FromResult(_authors.FirstOrDefault(x => x.Id == id));
    public Task AddAsync(Author author)
    {
        author.Id = _currentId++;
        _authors.Add(author);
        return Task.CompletedTask;
    }
    public Task UpdateAsync(Author author)
    {
        var indexToUpdate = _authors.FindIndex(x => x.Id == author.Id);
        _authors[indexToUpdate] = author;
        return Task.CompletedTask;
    }
    public Task DeleteAsync(long id)
    {
        _authors.RemoveAll(x => x.Id == id);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(long id) =>
        Task.FromResult(_authors.Any(author => author.Id == id));

    public Task<bool> ExistsByNameAndBirthDateAsync(string name, DateOnly birthDate, long? excludeId = null)
    {
        // По моей логике:
        // все таки авторы с одинаковыми именами бывают,
        // но, чтобы еще совпала дата рождения - 1 на 1,000,000,000
        var authors = _authors.Where(a => a.Name == name && a.DateOfBirth == birthDate);
        if (excludeId.HasValue)
        {
            authors = authors.Where(a => a.Id != excludeId.Value);
        }
        return Task.FromResult(authors.Any());
    }

    public Task<IEnumerable<Author>> GetAllWithBooksAsync()
    {
        foreach (var author in _authors)
        {
            author.Books = _books.Where(b => b.AuthorId == author.Id).ToList();
        }
        return Task.FromResult(_authors.AsEnumerable());
    }


    public Task<IEnumerable<Author>> GetByNameAsync(string name) =>
        Task.FromResult(_authors
                        .Where(author =>
                        author.Name.Contains(name)));

}
