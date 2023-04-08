using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorIntro.Pages;

public class IndexModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? CourseName { get; set; }

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger) => _logger = logger;

    public void OnGet()
    {
        if (string.IsNullOrWhiteSpace(CourseName)) CourseName = "Computer Science";
    }
}
