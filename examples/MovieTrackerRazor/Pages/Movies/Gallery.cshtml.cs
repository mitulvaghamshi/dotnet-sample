using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTrackerRazor.Data;
using MovieTrackerRazor.Models;

namespace MovieTrackerRazor.Pages.Movies;

public class GalleryModel : PageModel
{
    private readonly MovieTrackerContext _context;

    public GalleryModel(MovieTrackerContext context) => _context = context;

    public IList<Movie> Movie { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? MovieGenre { get; set; }

    public SelectList Genres { get; set; }

    public async Task OnGetAsync()
    {
        if (_context.Movie == null) return;

        SearchTerm ??= Request.RouteValues[nameof(SearchTerm)]?.ToString();
        MovieGenre ??= Request.RouteValues[nameof(MovieGenre)]?.ToString();

        var movies = from movie in _context.Movie select movie;
        var genres = from movie in movies select movie.Genre;

        Genres = new SelectList(await genres.Distinct().ToListAsync());

        if (!string.IsNullOrEmpty(SearchTerm))
        {
            movies = movies.Where(m => m.Title!.Contains(SearchTerm));
        }

        if (!string.IsNullOrEmpty(MovieGenre))
        {
            movies = movies.Where(m => m.Genre == MovieGenre);
        }

        Movie = await movies.AsNoTracking().ToListAsync();
    }
}
