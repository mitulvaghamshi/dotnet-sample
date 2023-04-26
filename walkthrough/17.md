# Walkthrough 17 - Razor Pages Introduction

## Setup

This will explore **Razor Pages**.

- Start Visual Studio.
- Click `Create a new project`.
- Set language to `C#` and project type to `Web`.
- Select the `ASP.NET Core Web App` template, click `Next`.
- Set Project name to `RazorIntro`.
- Set Location to a folder of your choosing.
- Ensure Place solution and project in the same directory is not selected, click
  `Next`.
- Set version to `.NET 5.0`, unselect `Configure for HTTPS`, click `Create`.

## Index.cshtml

- Open `Pages/Index.cshtml`.
- Notice the `@model` property is `IndexModel`.

## Index.cshtml.cs

- Open `Pages/Index.cshtml/Index.cshtml.cs`.
- Notice the class is `IndexModel` which _inherits_ from `PageModel`.
- Put the cursor on `PageModel` and press `F1`.
- Explore the help page.
- Notice the `OnGet` method.
- This method returns the `Index.cshtml` page.
- Add a property to the class.

```cs
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string FirstName { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
```

## Index.cshtml

- Display the property on the `Index` page.

```html
@page

@model IndexModel

@{ ViewData["Title"] = "Home page"; }

<div class="text-center">
    <h1 class="display-4">Welcome @Model.FirstName</h1>
    <p>Learn about
        <a href="https://docs.microsoft.com/aspnet/core">
            building Web apps with ASP.NET Core
        </a>.
    </p>
</div>
```

## Index.cshtml.cs

- At the moment, the `FirstName` property can only be _displayed_ on the `Index`
  page.
- To write to the property, it has to be marked as _writable_.

```cs
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public string FirstName { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
```

- The `FirstName` property could now be written to on a `Post` operation.
- In order to allow it to be specified in a URL (which would be a Get
  operation), it must be further annotated.

```cs
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty(SupportsGet = true)]
    public string FirstName { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
```

- Run the site.
- No name appears.
- Change the URL to http://localhost:12345/?FirstName=Bob.
- The name appears.
- Provide a default value for `FirstName` in `OnGet`.

```cs
public void OnGet()
{
    if (string.IsNullOrWhiteSpace(FirstName))
    {
        FirstName = "User";
    }
}
```

- Run the site, initially `User` will show up.
- Alter the URL to provide the `FirstName` parameter and it will appear again.
- This technique is only appropriate for non-sensitive data, typically
  identifiers.

## Index.cshtml

- Update the `@page` directive to optionally accept `FirstName` as **route
  data**.

```cs
@page "{FirstName?}"

@model IndexModel
// ...
```

- Save the file.
- Change the URL to http://localhost:12345/Betty.
- The name appears.

## AddCourse.cshtml

- Right-click the `Pages` folder and select `Add / New Folder`.
- Name the folder `Forms`.
- Right-click the `Forms` folder and select `Add / Razor Page...`
- Choose the `Razor Page - Empty` template, click **Add**.
- Set Razor Page name to `AddCourse`, click **Add**.
- Add a `Title` and an `h1` heading.

```html
@page

@model RazorIntro.Pages.Forms.AddCourseModel

@{ ViewBag.Title = "Add Course"; }

<h1>@ViewBag.Title</h1>
```

- Save the file.

## Course.cs

- Add a `Models` folder to the project.
- Add a class to the `Models` folder named `Course`.
- Add the `using System.ComponentModel.DataAnnotations;` directive.

```cs
public class Course
{
    [Display(Name = "Course Code")]
    public string CourseCode { get; set; }

    [Display(Name = "Course Name")]
    public string CourseName { get; set; }

    public int? Hours { get; set; }
}
```

- Save the file.

## AddCourse.cshtml.cs

- Add `Course` as a property and add the `using RazorIntro.Models;` directive.

```cs
public class AddCourseModel : PageModel
{
    [BindProperty]
    public Course Course { get; set; }

    public void OnGet()
    {
    }
}
```

## AddCourse.cshtml

- Add a form with text boxes for the 3 properties and a submit button.

```html
@page

@model RazorIntro.Pages.Forms.AddCourseModel

@{ ViewBag.Title = "Add Course"; }

<h1>@ViewBag.Title</h1>

<div class="col-md-4">
    <form method="post">
        <div class="form-group">
            <label asp-for="Course.CourseCode" class="col-form-label"></label>
            <input asp-for="Course.CourseCode" class="form-control" autofocus />
        </div>

        <div class="form-group">
            <label asp-for="Course.CourseName" class="col-form-label"></label>
            <input asp-for="Course.CourseName" class="form-control" />
        </div>

        <div class="form-group">
            <label asp-for="Course.Hours" class="col-form-label"></label>
            <input asp-for="Course.Hours" class="form-control" />
        </div>

        <button type="submit" class="btn btn-primary">Submit</button>
    </form>
</div>
```

- Save the file.
- Navigate to http://localhost:12345/forms/addcourse to see the new page.
- Close the browser.

## AddCourse.cshtml.cs

- Create a `Post` method

```cs
public IActionResult OnPost()
{
    if (!ModelState.IsValid)
    {
        return Page();
    }

    return RedirectToPage("Index");
}
```

## _Layout.cshtml

- Open `Pages/Shared/_Layout.cshtml`.
- Add a menu item for the new page.

```html
<!-- ... -->
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-page="/Index">Home</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-page="/Forms/AddCourse">Add Course</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-page="/Privacy">Privacy</a>
</li>
<!-- ... -->
```

## AddCourse.cshtml.cs

- Add a breakpoint to the `if (!ModelState.IsValid)` line.
- Run the site in debug mode.
- Add a course.
- When the debugger stops, hover over the `Course` property to see it is
  populated.
- Allow the program to continue.
- Stop the program, remove the breakpoint.

## Index.cshtml.cs

- Change the `FirstName` property to `CourseName`.

```cs
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty(SupportsGet = true)]
    public string CourseName { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        if (string.IsNullOrWhiteSpace(CourseName))
        {
            CourseName = "Computer Science";
        }
    }
}
```

- Save the file.

## Index.csthml

- Update the view to use `CourseName`.

```html
@page "{CourseName?}"

@model IndexModel

@{ ViewData["Title"] = "Home page"; }

<div class="text-center">
    <h1 class="display-4">Welcome to @Model.CourseName</h1>
    <p>Learn about
        <a href="https://docs.microsoft.com/aspnet/core">
            building Web apps with ASP.NET Core
        </a>.
    </p>
</div>
```

- Save the file.
- Run the site, the default greeting is presented.

## AddCourse.cshtml.cs

- Add a route value to the redirect as an anonymous object.

```cs
public IActionResult OnPost()
{
    if (ModelState.IsValid == false)
    {
        return Page();
    }

    return RedirectToPage("Index", new { CourseName = Course.CourseName });
}
```

- Since the property name of the parameter is the same as the property name, it
  can be removed.

```cs
public IActionResult OnPost()
{
    if (ModelState.IsValid == false)
    {
        return Page();
    }

    return RedirectToPage("Index", new { Course.CourseName });
}
```

- Run the site, add a new course.
