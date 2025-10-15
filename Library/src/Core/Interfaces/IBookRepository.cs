namespace Core.Interfaces;

public interface IBookRepository
{
    IEnumerable<Book> GetAllAsync();
    Book GetByIdAsync(int id);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);
}
