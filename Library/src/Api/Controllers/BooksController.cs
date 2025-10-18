using Microsoft.AspNetCore.Mvc;
using Library.Core.Interfaces;
using Library.Core.DTOs;
using Library.API.Controllers;

namespace Api.Controllers;

public class BooksController : BaseController
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType<BookDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook(long id)
    {
        var bookDto = await _bookService.GetBookByIdAsync(id);

        if (bookDto == null) return NotFound();

        return Ok(bookDto);
    }

    [HttpPost]
    [ProducesResponseType<BookDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createDto)
    {
        var createdBookDto = await _bookService.AddBookAsync(createDto);

        return CreatedAtAction(nameof(GetBook), new { id = createdBookDto.Id }, createdBookDto);
    }

}
