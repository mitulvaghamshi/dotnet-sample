using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTrackerRazor.Data;
using MovieTrackerRazor.Models;

namespace MovieTrackerRazor.Pages.Movies;

public class CreateModel : PageModel
{
    private readonly MovieTrackerContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    [ActivatorUtilitiesConstructor]
    public CreateModel(MovieTrackerContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _webHostEnvironment = environment;
    }

    public IActionResult OnGet() => Page();

    [BindProperty]
    public Movie Movie { get; set; } = default!;

    [BindProperty]
    public IFormFile? Upload { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || _context.Movie == null || Movie == null) return Page();

        if (Upload != null)
        {
            var file = Path.Combine(_webHostEnvironment.WebRootPath, "img", Path.GetFileName(Upload.FileName));

            using var fStream = new FileStream(file, FileMode.Create);
            await Upload.CopyToAsync(fStream);

            Movie.ImageFile = Path.GetFileName(Upload.FileName);
        }

        _context.Movie.Add(Movie);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
