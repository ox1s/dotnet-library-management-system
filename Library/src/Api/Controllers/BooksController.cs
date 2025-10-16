using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.DTOs;


namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBooksService _bookService;

    public BooksController(IBooksService authorService)
    {
        _bookService = authorService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType<BookDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook(long id)
    {
        var authorDto = await _bookService.GetBookByIdAsync(id); 
        
        if (authorDto == null) return NotFound();

        return Ok(authorDto);
    }

    // [HttpPost]
    // [ProducesResponseType<BookDto>(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createDto)
    // {
    //     var createdBookDto = await _bookService.AddBookAsync(createDto);

    //     return Ok(createdBookDto);
    // }

}
