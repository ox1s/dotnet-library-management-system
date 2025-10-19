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

    [HttpGet("{id}")]
    [ProducesResponseType<AuthorDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthor([FromRoute] long id)
    {
        var authorDto = await _authorService.GetAuthorByIdAsync(id);
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

    [HttpPut("{id}")]
    [ProducesResponseType<AuthorDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAuthor([FromRoute] long id, [FromBody] UpdateAuthorDto updateDto)
    {
        var existingAuthor = await _authorService.GetAuthorByIdAsync(id);
        var authorToUpdate = new AuthorDto(id, updateDto.Name, updateDto.DateOfBirth);
        await _authorService.UpdateAuthorInformationAsync(authorToUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<AuthorDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAuthor([FromRoute] long id)
    {
        await _authorService.DeleteAuthorAsync(id);
        return Ok();
    }

}
