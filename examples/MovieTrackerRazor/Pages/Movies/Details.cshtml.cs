using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieTrackerRazor.Data;
using MovieTrackerRazor.Models;

namespace MovieTrackerRazor.Pages.Movies;

public class DetailsModel : PageModel
{
    private readonly MovieTrackerContext _context;

    public DetailsModel(MovieTrackerContext context) => _context = context;

    public Movie Movie { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Movie == null) return NotFound();

        var movie = await _context.Movie.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();

        Movie = movie;

        return Page();
    }
}
