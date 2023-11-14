# Walkthrough 4 - Full MVC Application

## Setup

This walkthrough will fully implement an MVC application to track movies.

- Start Visual Studio, click `Create a new project`.
- Set language to `C#` and project type to `Web`.
- Select the `ASP.NET Core Web App (Model-View-Controller)` template, and click `Next`.
- Set Project name to `MovieTracker`, and click `Next`.
- Set version to .NET 5.0, unselect `Configure for HTTPS`, and click `Create`.
- Run the site and navigate around it.
- Close the browser.
- This site has been styled with [Bootstrap](https://getbootstrap.com/docs).

## Startup.cs

- Put a breakpoint on the constructor **F9** and press **F5** to run in debug mode.
- Hover over configuration and expand it and **Providers**,
- Check `appsettings.json` environment variables, command line parameters and more.
- Press **Shift+F5** to halt the debugger.
- Remove the breakpoint.

## Movie.cs

- In the `Models` folder, create a new class named `Movie.cs`.
- The question mark after `DateTime` and int indicates that `DateSeen` and `Rating` are _nullable_ types, or _optional_.

```cs
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime? DateSeen { get; set; }
    public string Genre { get; set; }
    public int? Rating { get; set; }
}
```

- Add a `Key` _annotation_ to `Id`.
- Use quick actions to add the `using System.ComponentModel.DataAnnotations;` directive.

```cs
public class Movie
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime? DateSeen { get; set; }
    public string Genre { get; set; }
    public int? Rating { get; set; }
}
```

- Add `Required` and `Range` _annotations_.

```cs
public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public DateTime? DateSeen { get; set; }

    public string Genre { get; set; }

    [Range(1, 10)]
    public int? Rating { get; set; }
}
```

- Save the file.

## MoviesController.cs

- In Solution Explorer, right-click the `Controllers` folder and select `Add / Controller...`.
- Select the `MVC Controller with read/write actions` template, click `Add` and name it `MoviesController`.
- Add a list of `movies` to act as data, add the `using MovieTracker.Models;` directive.

```cs
public class MoviesController : Controller
{
    private static List<Movie> movies = new List<Movie>
    {
        new Movie
        {
            Id = 1,
            Title = "Birds of Prey",
            DateSeen = DateTime.Now.AddDays(-50),
            Genre = "Action",
            Rating = 6
        },
        new Movie
        {
            Id = 2,
            Title = "Palm Springs",
            DateSeen = DateTime.Now.AddDays(-25),
            Rating = 7
        },
        new Movie
        {
            Id = 3,
            Title = "Hamilton",
            Genre = "Drama"
        }
    };

    // GET: Movies
    public ActionResult Index()
    // ...
```

- Right-click in the `Index` method and select `Add View...`.
- Select `Razor View`, click `Add`.
- Set `Template` to `List`.
- Set `Model` class to `Movie (MovieTracker.Models)`, and click `Add`.
- At the bottom of `Index.cshtml`, notice the three links that have commented placeholders for the primary keys.
- Return to `MoviesController.cs` and update the `Index` method to return the list of `movies`.

```cs
public ActionResult Index()
{
    return View(movies);
}
```

- Press **Ctrl+F5** to launch the site.
- Browse to `http://localhost:<port>/movies` to see the list of movies.

## _Layout.cshtml

- Press **Ctrl+T** to open the all-purpose Go to tool.
- Begin typing `_Layout`, select it when its recognized.
- Update the title and application link.
- Also add a link to the `MoviesController / Index` action.
- Finally, update the footer.

```html
<!-- ... -->
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - Movie Tracker</title>
    <!-- ... -->
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container">
                <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">Movie Tracker</a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Movies" asp-action="Index">Movies</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
                        </li>
                    </ul>
                </div>
                <!-- ... -->
<!-- ... -->
<footer class="border-top footer text-muted">
    <div class="container">
        &copy; @DateTime.Now.Year - Movie Tracker - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
    </div>
</footer>
<!-- ... -->
```

- Save the file, refresh the browser.
- Hover over the `Edit`, `Details` and `Delete` links and note that they are lacking the routing information to access a specific movie.

## Index.cshtml

- Update the links so that they will use the movie's `Id`.

```cs
// ...
<td>
    @Html.ActionLink("Edit", "Edit", new { id = item.Id }) |
    @Html.ActionLink("Details", "Details", new { id = item.Id }) |
    @Html.ActionLink("Delete", "Delete", new { id = item.Id })
</td>
// ...
```

- Save the file, refresh the browser.
- Hover the links again; they are now correct.
- If you click any of them you will receive an error message, because none of those views exist yet.

## MoviesController.cs

- Update the `Details` method to find the selected movie and return it.

```cs
public ActionResult Details(int id)
{
    var movie = movies.Find(m => m.Id == id);

    return View(movie);
}
```

- Save the file.
- Right-click in the `Details` method and select `Add View...`.
- Select Razor View, and click `Add`.
- Set `Template` to `Details`.
- Set `Model` class to `Movie (MovieTracker.Models)`, and click `Add`.

## Details.cshtml

- Update the `Edit` link.

```html
<!-- ... -->
<div>
    @Html.ActionLink("Edit", "Edit", new { id = Model.Id }) |
    <a asp-action="Index">Back to List</a>
</div>
<!-- ... -->
```

- Save the file, refresh the browser.
- Click one of the `Details` links.
- Notice that the time displays for `movies` and that the heading is `DateSeen`.

## Movie.cs

- Add additional annotations to tidy the display.

```cs
public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Date Seen")]
    public DateTime? DateSeen { get; set; }

    public string Genre { get; set; }

    [Range(1, 10)]
    public int? Rating { get; set; }
}
```

- Alternatively, multiple attributes can be specified in one annotation.

```cs
public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [DataType(DataType.Date)], Display(Name = "Date Seen")]
    public DateTime? DateSeen { get; set; }

    public string Genre { get; set; }

    [Range(1, 10)]
    public int? Rating { get; set; }
}
```

- Save the file, refresh the browser.
- Note the improved output in the browser.
- Click the `Back` to List link and notice that the model updates appear here too.

## MoviesController.cs

- Update the method signature of the `POST` / `Create` method to accept a `Movie` object instead of the more general `IFormCollection` object.
- Verify that the model state is valid and if so add the movie to the list;
- If not to return the `View` with the movies.

```cs
public ActionResult Create(Movie movie)
{
    try
    {
        if (!ModelState.IsValid) { return View(movie); }

        movies.Add(movie);

        return RedirectToAction(nameof(Index));
    }
    catch
    {
        return View();
    }
}
```

- Save the file.
- Right-click in the `Create` method and select `Add View...`.
- Select `Razor View`, click `Add`.
- Set `Template` to `Create`.
- Set `Model` class to `Movie (MovieTracker.Models)`, and click `Add`.
- Refresh the browser.
- Click the `Create New` link.
- Add a new movie.

## Create.cshtml

- Add an `autofocus` attribute to the input tag for `Id`.

```html
<!-- ... -->
<div class="form-group">
    <label asp-for="Id" class="control-label"></label>
    <input asp-for="Id" class="form-control" autofocus />
    <span asp-validation-for="Id" class="text-danger"></span>
</div>
<!-- ... -->
```

- Save the file.
- Click the `Create New` link again. Notice focus in the `Id` text box.
- Click the `Create` button right away to see the validation errors.
- Correct the errors.
- Set the rating higher than 10, click the `Create` button.
- Click the `Back` to List link.

## MoviesController.cs

- Add a method to get a movie, knowing the `id`.

```cs
private Movie GetMovie(int id) => movies.Find(m => m.Id == id);
```

- Update the `GET` `Edit` method to find the selected movie and return it.

```cs
public ActionResult Edit(int id)
{
    var movie = GetMovie(id);

    return View(movie);
}
```

- Update the `Details` method to also use the new method.

```cs
public ActionResult Details(int id)
{
    // var movie = movies.Find(m => m.Id == id);

    var movie = GetMovie(id);

    return View(movie);
}
```

- Update the method signature of the `POST` / `Edit` method to accept a `Movie` object instead of an `IFormCollection` object.
- Verify that the model state is valid and if so find the index of the selected movie and update the list with it;
- If not to return the `View` with the movie.

```cs
public ActionResult Edit(int id, /* IFormCollection collection */Movie movie)
{
    try
    {
        if (ModelState.IsValid)
        {
            var index = movies.FindIndex(m => m.Id == id);

            movies[index] = movie;

            return RedirectToAction(nameof(Index));
        }
        else
        {
            return View(movie);
        }
    }
    catch
    {
        return View();
    }
}
```

- Save the file.
- Right-click in the `Edit` method and select `Add View...`.
- Select `Razor View`, and click `Add`.
- Set `Template` to `Edit`.
- Set `Model` class to `Movie (MovieTracker.Models)`, and click `Add`.

## Edit.cshtml

- Add an `autofocus` attribute to the input tag for `Id`.

```html
<!-- ... -->
<div class="form-group">
    <label asp-for="Id" class="control-label"></label>
    <input asp-for="Id" class="form-control" autofocus />
    <span asp-validation-for="Id" class="text-danger"></span>
</div>
<!-- ... -->
```

- Save the file, refresh the browser.
- Edit one of the movies.

## MoviesController.cs

- Update the `GET` / `Delete` method to find the selected movie and return it.

```cs
public ActionResult Delete(int id)
{
    var movie = GetMovie(id);

    return View(movie);
}
```

- Add a method to get the movie's index in the array, knowing the `id`.

```cs
private int GetMovieIndex(int id) => movies.FindIndex(m => m.Id == id);
```

- Update the `POST` / `Edit` method to also use the new method.

```cs
public ActionResult Edit(int id, Movie movie)
{
    try
    {
        if (ModelState.IsValid)
        {
            var index = GetMovieIndex(id);

            movies[index] = movie;

            return RedirectToAction(nameof(Index));
        }
        else
        {
            return View(movie);
        }
    }
    catch
    {
        return View();
    }
}
```

- Update the `POST` / `Delete` method to find the index of the selected movie and remove it from the list.

```cs
public ActionResult Delete(int id, IFormCollection collection)
{
    try
    {
        var index = GetMovieIndex(id);

        movies.RemoveAt(index);

        return RedirectToAction(nameof(Index));
    }
    catch
    {
        return View();
    }
}
```

- Save the file.
- Right-click in the `Delete` method and select `Add View...`.
- Select `Razor View`, click `Add`.
- Set `Template` to `Delete`.
- Set `Model` class to `Movie (MovieTracker.Models)`, and click `Add`.
- Refresh the browser.
- Delete one of the movies.
