using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.DTOs;


namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthor(long id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        if (author == null) return NotFound();
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO authorDTO)
    {
        var author = await _authorService.AddAuthorAsync(authorDTO);
        return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
    }

}
