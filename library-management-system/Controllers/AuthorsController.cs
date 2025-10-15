
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using library_management_system.Models;

namespace library_management_system.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly AuthorContext _context;

    public AuthorsController(AuthorContext context)
    {
        _context = context;
    }

    // GET: api/Authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthor()
    {
        return await _context.Authors
            .Select(x => new AuthorDTO(x))
            .ToListAsync();
    }

    // GET: api/Authors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDTO>> GetAuthor(long id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null) return NotFound();

        return AuthorToDTO(author);
    }

     private static AuthorDTO AuthorToDTO(Author author) =>
       new AuthorDTO
       {
           Id = author.Id,
           Name = author.Name,
           DateOfBirth = author.DateOfBirth
       };
}
