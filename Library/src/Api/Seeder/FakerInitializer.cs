using Bogus;
using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Library.DAL.DataContext;

namespace Library.DAL.Seeder;

public class FakerInitializer
{
    private readonly LibraryDbContext _context;
    public FakerInitializer(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        await _context.Database.MigrateAsync();

        if (await _context.Authors.AnyAsync())
        {
            return;
        }

        var authorFaker = new Faker<Author>("ru")
            .RuleFor(a => a.Name, f => f.Name.FullName())
            .RuleFor(a => a.DateOfBirth, f => f.Date.PastDateOnly(50, new DateOnly(1970, 1, 1)));

        var authors = authorFaker.Generate(20);
        await _context.Authors.AddRangeAsync(authors);
        await _context.SaveChangesAsync();

        var bookFaker = new Faker<Book>("ru")
            .RuleFor(b => b.Title, f => f.Lorem.Sentence(3, 2))
            .RuleFor(b => b.PublishedYear, f => f.Date.Past(30, DateTime.Now.AddYears(-1)).Year);

        var allBooks = new List<Book>();

        foreach (var author in authors)
        {
            var booksForAuthor = bookFaker.Generate(new Faker().Random.Number(1, 5));
            foreach (var book in booksForAuthor)
            {
                book.AuthorId = author.Id;
                if (book.PublishedYear < author.DateOfBirth.Year)
                {
                    book.PublishedYear = new Faker().Random.Number(author.DateOfBirth.Year, DateTime.Now.Year);
                }
            }
            allBooks.AddRange(booksForAuthor);
        }

        await _context.Books.AddRangeAsync(allBooks);

        await _context.SaveChangesAsync();
    }
}