using Microsoft.AspNetCore.Mvc;
using Library.Core.Interfaces;
using Library.Core.DTOs;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(long id)
    {
        var bookDto = await _bookService.GetBookByIdAsync(id);
        return Ok(bookDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks(int? year)
    {
        if (year == null)
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }
        else
        {
            var books = await _bookService.GetAllBooksAfterYearAsync((int)year);
            return Ok(books);
        }

    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createDto)
    {
        var createdBookDto = await _bookService.AddBookAsync(createDto);

        return CreatedAtAction(nameof(GetBook), new { id = createdBookDto.Id }, createdBookDto);
    }


}
