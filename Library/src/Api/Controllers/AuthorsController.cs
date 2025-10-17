using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.DTOs;


namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorsService _authorService;

    public AuthorsController(IAuthorsService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType<AuthorDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthor(long id)
    {
        var authorDto = await _authorService.GetAuthorByIdAsync(id);

        if (authorDto == null) return NotFound();

        return Ok(authorDto);
    }

    [HttpPost]
    [ProducesResponseType<AuthorDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto createDto)
    {
        var createdAuthorDto = await _authorService.AddAuthorAsync(createDto);

        return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthorDto.Id }, createdAuthorDto);
    }

}
