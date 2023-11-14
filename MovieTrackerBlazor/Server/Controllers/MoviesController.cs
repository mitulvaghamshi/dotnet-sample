using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTrackerBlazor.Server.Data;
using MovieTrackerBlazor.Shared;

namespace MovieTrackerBlazor.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieTrackerContext _context;

    public MoviesController(MovieTrackerContext context) => _context = context;

    // GET: Movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
    {
        if (_context.Movie == null) return NotFound();

        return await _context.Movie.Include(m => m.Genre).AsNoTracking().ToListAsync();
    }

    // GET: Movies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        if (_context.Movie == null) return NotFound();

        var movie = await _context.Movie.FindAsync(id);

        if (movie == null) return NotFound();

        return movie;
    }

    // PUT: Movies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, Movie movie)
    {
        if (id != movie.Id) return BadRequest();

        _context.Entry(movie).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieExists(id)) return NotFound(); else throw;
        }
        return NoContent();
    }

    // POST: Movies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
    {
        if (_context.Movie == null) return Problem("Entity set 'MovieTrackerContext.Movie' is null.");

        _context.Movie.Add(movie);

        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
    }

    // DELETE: Movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        if (_context.Movie == null) return NotFound();

        var movie = await _context.Movie.FindAsync(id);

        if (movie == null) return NotFound();

        _context.Movie.Remove(movie);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MovieExists(int id) => (_context.Movie?.AsNoTracking().Any(e => e.Id == id)).GetValueOrDefault();
}
