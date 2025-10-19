using Microsoft.AspNetCore.Mvc;
using Library.Core.Interfaces;
using Library.Core.DTOs;


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

    [HttpGet]
    public async Task<IActionResult> GetAuthors(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            var authors = await _authorService.GetAuthorsByNameAsync(name);
            return Ok(authors);
        }
        else
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthor(long id)
    {
        var authorDto = await _authorService.GetAuthorByIdAsync(id);
        return Ok(authorDto);
    }


    [HttpPost]
    public async Task<IActionResult> CreateAuthor(CreateAuthorDto createDto)
    {
        var createdAuthorDto = await _authorService.AddAuthorAsync(createDto);

        return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthorDto.Id }, createdAuthorDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(long id, UpdateAuthorDto authorDto)
    {
        await _authorService.UpdateAuthorInformationAsync(id, authorDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(long id)
    {
        await _authorService.DeleteAuthorAsync(id);
        return NoContent();
    }

    // EF Запросы
    [HttpGet("with-book-count")]
    public async Task<IActionResult> GetAuthorsWithBookCount()
    {
        var authors = await _authorService.GetAllAuthorsWithBookCountAsync();
        return Ok(authors);
    }
}
