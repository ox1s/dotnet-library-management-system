using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers;

[ApiController]
public class AuthorsController : ControllerBase
{
    List<string> _author = new List<string>
    {
        "Андерс Хейлсберг",
        "Джеффри Рихтер",
        "Джозеф и Бен Албахари",
        "Эндрю Лок"
    };
    [HttpGet("author")]
    public IEnumerable<string> Index() => _author;

    [HttpPost("author")]
    public ActionResult Update(UpdateModel model)
    {
        if (model.Id < 0 || model.Id > _author.Count)
            return NotFound();
        _author[model.Id] = model.Name;
        return Ok();
    }


    [HttpGet("author/{id}")]
    public ActionResult<string> View(int id) =>
        (id >= 0 && id < _author.Count)
        ? _author[id]
        : NotFound();
}
