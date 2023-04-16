using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTrackerBlazor.Server.Data;
using MovieTrackerBlazor.Shared;

namespace MovieTrackerBlazor.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly MovieTrackerContext _context;

    public GenresController(MovieTrackerContext context) => _context = context;

    // GET: Genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenre()
    {
        if (_context.Genre == null) return NotFound();

        return await _context.Genre.AsNoTracking().ToListAsync();
    }
}
