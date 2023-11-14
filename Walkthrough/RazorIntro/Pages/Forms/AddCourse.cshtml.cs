using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RazorIntro.Pages.Forms;

public class AddCourseModel : PageModel
{
    [BindProperty]
    public CourseModel Course { get; set; } = default!;

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        return RedirectToPage("/Index", new { Course.CourseName });
    }

    public class CourseModel
    {
        [Required]
        [DisplayName("Course Code")]
        public string? CourseCode { get; set; }

        [Required]
        [DisplayName("Course Name")]
        public string? CourseName { get; set; }

        [DisplayName("Course Duration"), DataType(DataType.Duration)]
        public int? Hours { get; set; }
    }
}
