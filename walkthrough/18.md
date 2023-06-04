# Walkthrough 18 - Razor Pages with EF Core

## Setup

This will add **database** access to a **Razor Pages** site.

- Start SQL Server.
- Start Visual Studio.
- Click `Create a new project`.
- Set language to `C#` and project type to `Web`.
- Select the `ASP.NET Core Web App` template, click `Next`.
- Set Project name to `MovieTrackerRazor`.
- Set Location to a folder of your choosing.
- Ensure Place solution and project in the same directory is not selected, click
  `Next`.
- Set version to `.NET 5.0`, unselect `Configure for HTTPS`, click `Create`.

## Movie.cs

- Create a `Models` folder.
- Create a `Movie` class in the `Models` folder.
- Add the `using System.ComponentModel.DataAnnotations;` directive.

```cs
public class Movie
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }

    [DataType(DataType.Date), Display(Name = "Date Seen")]
    public DateTime? DateSeen { get; set; }

    public string Genre { get; set; }

    [Range(1, 10)]
    public int? Rating { get; set; }

    [Display(Name = "Image File")]
    public string ImageFile { get; set; }
}
```

- Save the file.

## Database Scaffolding

- Under the `Pages` folder, create a folder named `Movies`.
- Right-click the `Movies` folder and select `Add / New Scaffolded Item...`
- Select `Razor Pages`, then `Razor Pages using Entity Framework (CRUD)`, click
  **Add**.
- Set `Model` class to `Movie (MovieTrackerRazor.Models)`.
- For `Data context` class, click the `+` button.
- Accept the name `MovieTrackerRazor.Data.MovieTrackerRazorContext`, click
  **Add**.
- Click **Add**.
- The _Scaffolder_ will create a `.cshtml` and `.cshtml.cs` file for `Create`,
  `Delete`, `Details`, `Edit` and `Index`.
- Briefly examine the files.
- It will also register the **database** in `Startup.cs`.

## appsettings.json

- Update the _connection string_ to use **SQL Server Express** and simplify the
  database name.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "MovieTrackerRazorContext": "Server=localhost\\sqlexpress;Database=movie_tracker_razor;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

- Save the file.

## Database Migration

- In the **Package Manager Console**, issue the commands:

```console
Add-Migration init
Update-Database
```

## _Layout.cshtml

- Open `Pages/Shared/_Layout.cshtml`.
- Change the `Home` menu entry to link to the `Movie` page.

```html
<!-- ... -->
<li class="nav-item">
    <a class="nav-link text-dark" asp-area=""
        asp-page="/Movies/Index">Movies</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area=""
        asp-page="/Privacy">Privacy</a>
</li>
<!-- ... -->
```

- Save the file.

## SeedData.cs

- Add a class named `SeedData` to the `Models` folder.
- Make the class `static` and add an _initializer_ method.
- Add the `using MovieTrackerRazor.Data;` directive.

```cs
public static class SeedData
{
    public static void Initialize(MovieTrackerRazorContext context)
    {
    }
}
```

- Have the initializer add 5 movies.

```cs
public static void Initialize(MovieTrackerRazorContext context)
{
    context.Movie.AddRange(
        new Movie
        {
            Title = "The Shawshank Redemption",
            DateSeen = DateTime.Now.AddDays(-150).Date,
            Genre = "Drama",
            Rating = 8,
            ImageFile = "shawshank.jpg"
        },
        new Movie
        {
            Title = "Men in Black",
            DateSeen = DateTime.Now.AddDays(-250).Date,
            Genre = "Action",
            Rating = 7,
            ImageFile = "meninblack.jpg"
        },
        new Movie
        {
            Title = "The Dark Knight",
            DateSeen = DateTime.Now.AddDays(-350).Date,
            Genre = "Action",
            Rating = 9,
            ImageFile = "darkknight.jpg"
        },
        new Movie
        {
            Title = "12 Angry Men",
            DateSeen = DateTime.Now.AddDays(-450).Date,
            Genre = "Drama",
            Rating = 7,
            ImageFile = "12angrymen.jpg"
        },
        new Movie
        {
            Title = "Back to the Future",
            DateSeen = DateTime.Now.AddDays(-550).Date,
            Genre = "Adventure",
            Rating = 8,
            ImageFile = "backtofuture.jpg"
        }
    );

    context.SaveChanges();
}
```

- Save the file.

## Index.cshtml.cs

- Update the constructor in `Pages / Movies / Index.csthml.cs` to seed the
  database if necessary.
- Add `using MovieTrackerRazor.Data.MovieTrackerRazorContext;` directive.

```cs
public IndexModel(MovieTrackerRazorContext context)
{
    _context = context;

    if (!_context.Movie.Any())
    {
        SeedData.Initialize(_context);
    }
}
```

- Run the site and access the movies.
- Add a property to `IndexModel` to support filtering.

```cs
public class IndexModel : PageModel
{
    private readonly MovieTrackerRazorContext _context;

    public IndexModel(MovieTrackerRazorContext context)
    {
        _context = context;

        if (!context.Movie.Any())
        {
            SeedData.Initialize(context);
        }
    }

    public IList<Movie> Movie { get;set; }

    [BindProperty(SupportsGet = true)]
    public string SearchString { get; set; }

    public async Task OnGetAsync()
    {
        Movie = await _context.Movie.ToListAsync();
    }
}
```

- Update `OnGetAsync` to filter the movies if a search string is specified.

```cs
public async Task OnGetAsync()
{
    var movies = from m in _context.Movie select m;

    if(!string.IsNullOrEmpty(SearchString))
    {
        movies = movies.Where(m => m.Title.Contains(SearchString));
    }

    Movie = await _context.Moviemovies.ToListAsync();
}
```

- Run the site, navigate to movies and change the URL to
  http://localhost:12345/Movies?searchstring=men.

## Index.csthml

- Add a text field and button to allow filtering.

```html
<!-- ... -->
<p>
    <a asp-page="Create">Create New</a>
</p>

<form>
    <p>
        Title: <input type="text" asp-for="SearchString" />
        <input type="submit" value="Filter" class="btn btn-primary" />
    </p>
</form>

<table class="table">
<!-- ... -->
```

- Run the site and search by title.

## Index.csthml.cs

- Add two more properties to `IndexModel` to support a genre drop-down list, add
  the `using Microsoft.AspNetCore.Mvc.Rendering;` directive.

```cs
// ...
[BindProperty(SupportsGet = true)]
public string SearchString { get; set; }

public SelectList Genres { get; set; }

[BindProperty(SupportsGet = true)]
public string MovieGenre { get; set; }

public async Task OnGetAsync()
// ...
```

- Update `OnGetAsync` to prepare a list of genres.

```cs
public async Task OnGetAsync()
{
    // Use LINQ to get a list of genres
    IQueryable<string> genreQuery =
        from m in _context.Movie
        orderby m.Genre
        select m.Genre;

    Genres = new SelectList(await genreQuery
        .Distinct().ToListAsync());

    var movies = from m in _context.Movie select m;

    if(!string.IsNullOrEmpty(SearchString))
    {
        movies = movies.Where(m =>
            m.Title.Contains(SearchString));
    }

    if (!string.IsNullOrEmpty(MovieGenre))
    {
        movies = movies.Where(m => m.Genre == MovieGenre);
    }

    Movie = await movies.ToListAsync();
}
```

- Save the file.

## Index.cshtml

- Add a drop-down list for genre.

```html
<!-- ... -->
<form>
    <p>
        Title:
        <input type="text" asp-for="SearchString" />
        <input type="submit" value="Filter" class="btn btn-primary" />
        Genre:
        <select asp-for="MovieGenre"
            asp-items="Model.Genres"
            onchange="submit()">
            <option value="">All</option>
        </select>
    </p>
</form>
<!-- ... -->
```

- Run the site and filter by `Title` and/or `Genre`.

## Images

- In the `wwwroot` folder, create a new folder named `img`.
- Copy content of [img](../examples/MovieTrackerRazor/wwwroot/img/) folder and
  paste into newly created `img` folder of your project.

## Index.cshtml

- The table is going to be replaced with the
  [Bootstrap card system](https://getbootstrap.com/docs/4.1/components/card/) to
  layout the movies.
- Delete the existing table.

```html
<!-- ... -->
</form>

<!-- <table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.Movie[0].Title)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Movie[0].DateSeen)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Movie[0].Genre)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Movie[0].Rating)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Movie[0].ImageFile)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model.Movie) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.Title)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.DateSeen)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Genre)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Rating)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.ImageFile)
            </td>
            <td>
                <a asp-page="./Edit" asp-route-id="@item.Id">Edit</a> |
                <a asp-page="./Details" asp-route-id="@item.Id">Details</a> |
                <a asp-page="./Delete" asp-route-id="@item.Id">Delete</a>
            </td>
        </tr>
        }
    </tbody>
</table> -->
```

- Replace the table with a card presentation.

```html
<!-- ... -->
</form>

<div class="card-columns">
    @foreach (var item in Model.Movie)
    {
        <a asp-page="Details" asp-route-id="@item.Id">
            <div class="card" style="width: 18rem;">
                <img src="~/img/@item.ImageFile"
                    class="card-img-top" />
                <div class="card-body">
                    <h5 class="card-title text-center text-body">
                        @item.Title
                    </h5>
                </div>
            </div>
        </a>
    }
</div>
```

- Run the site.

## Create.cshtml.cs

- Create a property for the host environment, add the
  `using Microsoft.AspNetCore.Hosting;` directive.
- Modify the constructor to accept the host environment and assign it.

```cs
public class CreateModel : PageModel
{
    private readonly MovieTrackerRazorContext _context;
    private readonly IWebHostEnvironment _environment;

    public CreateModel(
        MovieTrackerRazorContext context,
        IWebHostEnvironment environment
    )
    {
        _context = context;
        _environment = environment;
    }

    public IActionResult OnGet()
    // ...
```

- Add an `IFormFile` property named `Upload`, add the
  `using Microsoft.AspNetCore.Http;` directive.

```cs
// ...
public IActionResult OnGet()
{
    return Page();
}

[BindProperty]
public Movie Movie { get; set; }

[BindProperty]
public IFormFile Upload { get; set; }

public async Task<IActionResult> OnPostAsync()
// ...
```

- Update `OnPostAsync` to upload the file to the images folder and to update the
  `ImageFile` with the file name.
- Add the `using System.IO;` directive.

```cs
public async Task<IActionResult> OnPostAsync()
{
    if (!ModelState.IsValid)
    {
        return Page();
    }

    if (Upload != null)
    {
        var file = Path.Combine(_environment.WebRootPath,
            "images", Path.GetFileName(Upload.FileName));

        using (var fileStream =
            new FileStream(file, FileMode.Create))
        {
            await Upload.CopyToAsync(fileStream);
        }

        Movie.ImageFile = Path.GetFileName(Upload.FileName);
    }

    _context.Movie.Add(Movie);

    await _context.SaveChangesAsync();

    return RedirectToPage("Index");
}
```

- Save the file.

## Create.cshtml

- Modify the form tag to allow the uploading of files.

```html
<!-- ... -->
<div class="col-md-4">
    <form method="post" enctype="multipart/form-data">
        <div asp-validation-summary="ModelOnly"
             class="text-danger"></div>
        <div class="form-group">
        <!-- ... -->
```

- Change the `ImageFile` property to be an `Upload` control.

```html
<!-- ... -->
<div class="form-group">
    <label asp-for="Movie.ImageFile" class="control-label"></label>
    <!-- <input asp-for="Movie.ImageFile" class="form-control" /> -->
    <input type="file" asp-for="Upload" />
    <span asp-validation-for="Movie.ImageFile" class="text-danger"></span>
</div>
<!-- ... -->
```

- Search the web for a movie poster and download it.
- Run the site.
- Add a movie with the downloaded poster.
- Examine the `wwwroot/images` folder and notice that the new image is there.
